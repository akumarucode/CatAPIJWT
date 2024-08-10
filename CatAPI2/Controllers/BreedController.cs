using AutoMapper;
using CatAPI2.Dto;
using CatAPI2.Interfaces;
using CatAPI2.Models;
using CatAPI2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace CatAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly IBreedRepository _breedRepository;
        private readonly IMapper _mapper;

        public BreedController (IBreedRepository breedRepository,IMapper mapper)
        {
            _breedRepository = breedRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(200,Type = typeof(IEnumerable<Breed>))] 
        public IActionResult GetBreeds()
        {
            var breeds = _mapper.Map<List<BreedDto>>(_breedRepository.GetBreeds());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);  
            }

            return Ok(breeds);
        }

        [HttpGet("{breedId}")]
        [ProducesResponseType(200, Type = typeof(Breed))]
        [ProducesResponseType(400)]
        public IActionResult GetBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();

            var breed = _mapper.Map<BreedDto>(_breedRepository.GetBreed(breedId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(breed);
        }

        //[HttpGet("{breedName}")]
        //[ProducesResponseType(200, Type = typeof(Breed))]
        //[ProducesResponseType(400)]
        //public IActionResult GetBreedByName(string breedName)
        //{
        //    if (!_breedRepository.BreedExistsByName(breedName))
        //        return NotFound();

        //    var breed = _mapper.Map<BreedDto>(_breedRepository.GetBreedByName(breedName));

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    return Ok(breed);
        //}


        //[HttpGet("/cats/{catId}")]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(200, Type = typeof(Breed))]
        //public IActionResult GetBreedOfACat(int catId)
        //{
        //    var breed = _mapper.Map<BreedDto>(_breedRepository.GetBreedByCat(catId));

        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    return Ok(breed);
        //}

        [HttpGet("{breedId}/GetCatsByBreed")]
        [ProducesResponseType(200, Type = typeof(Cat))]
        [ProducesResponseType(400)]

        public IActionResult GetCatsByBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
            {
                return NotFound();
            }

            var cats = _mapper.Map<List<CatDto>>(_breedRepository.GetCatsByBreed(breedId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cats);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBreed([FromBody] BreedDto breedDetails)
        {
            if (breedDetails == null)
                return BadRequest(ModelState);

            var breed = _breedRepository.GetBreeds()
                .Where(c => c.Name.Trim().ToUpper() == breedDetails.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (breed != null)
            {
                ModelState.AddModelError("", "Breed already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var breedMap = _mapper.Map<Breed>(breedDetails);


            if (!_breedRepository.CreateBreed(breedMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpDelete("{breedId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteBreed(int breedId)
        {
            if (!_breedRepository.BreedExists(breedId))
                return NotFound();

            var breedToDelete = _breedRepository.GetBreed(breedId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_breedRepository.DeleteCategory(breedToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting breed");
            }

            return NoContent();
        }

        //UPDATE
        [HttpPut("{breedId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateBreed(int breedId, [FromBody] BreedDto updatedBreed)
        {

            if (updatedBreed == null)
                return BadRequest(ModelState);

            if (breedId != updatedBreed.Id)
                return BadRequest(ModelState);

            if (!_breedRepository.BreedExists(breedId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var breedMap = _mapper.Map<Breed>(updatedBreed);

            if (!_breedRepository.UpdateBreed(breedMap))
            {
                ModelState.AddModelError("", "Something went wrong updating breed");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
