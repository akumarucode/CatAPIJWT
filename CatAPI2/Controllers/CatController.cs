using AutoMapper;
using CatAPI2.Dto;
using CatAPI2.Interfaces;
using CatAPI2.Models;
using CatAPI2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatAPI2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatRepository _catRepository;
        private readonly IMapper _mapper;

        public CatController(ICatRepository catRepository, IMapper mapper)
        {
            _catRepository = catRepository;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Cat>))]
        public IActionResult GetCats()
        {

            var cats = _mapper.Map<List<CatDto>>(_catRepository.GetCats());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(cats);
        }

        [HttpGet("{catId}")]
        [Authorize]
        [ProducesResponseType(200, Type = typeof(Cat))]
        [ProducesResponseType(400)]
        public IActionResult GetCat(int catId)
        {
            if (!_catRepository.CatExists(catId))
                return NotFound();

            var cat = _mapper.Map<CatDto>(_catRepository.GetCat(catId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(cat);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCat([FromQuery] int ownerId, [FromQuery] int breedId,[FromBody] CatDto createCat)
        {
            if (createCat == null)
                return BadRequest(ModelState);

            var cat = _catRepository.GetCats()
                .Where(c => c.Name.Trim().ToUpper() == createCat.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (cat != null)
            {
                ModelState.AddModelError("", "Cat already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var catMap = _mapper.Map<Cat>(createCat);

            if (!_catRepository.CreateCat(ownerId, breedId, catMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpDelete("{catId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteCat(int catId)
        {
            if (!_catRepository.CatExists(catId))
                return NotFound();

            var catToDelete = _catRepository.GetCat(catId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_catRepository.DeleteCat(catToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting cat");
            }

            return NoContent();
        }


        //UPDATE
        [HttpPut("{catId}")]
        [Authorize]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateCat(int catId, [FromQuery] int ownerId, [FromQuery] int breedId, [FromBody] CatDto updatedCat)
        {

            if (updatedCat == null)
                return BadRequest(ModelState);

            if (catId != updatedCat.Id)
                return BadRequest(ModelState);

            if (!_catRepository.CatExists(catId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var catMap = _mapper.Map<Cat>(updatedCat);

            if (!_catRepository.UpdateCat(ownerId,breedId,catMap))
            {
                ModelState.AddModelError("", "Something went wrong updating cat");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
