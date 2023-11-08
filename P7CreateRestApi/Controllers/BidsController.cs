﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Domain;
using P7CreateRestApi.Repositories;

namespace P7CreateRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidsController : ControllerBase
    {
        private readonly IBidRepository _bidRepository;

        public BidsController(IBidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }

        // GET: api/Bids
        [Authorize(Roles = "Admin, Creator, Updator, SimpleUser")]
        [HttpGet("Get All Bids")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
                public async Task<ActionResult<IEnumerable<Bid>>> GetBids()
        {
            var bids = await _bidRepository.GetAllBids();
            if (bids == null)
            {
                return NotFound();
            }
            return Ok(bids);
        }

        // GET: api/Bids/5
        [Authorize(Roles = "Admin, Creator, Updator, SimpleUser")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> GetBid(int id)
        {
            var bid = await _bidRepository.GetBidByIdAsync(id);

            if (bid == null)
            {
                return NotFound();
            }

            return Ok(bid);
        }

        // PUT: api/Bids/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin, Updator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutBid(int id, Bid bid)
        {
            if (id != bid.BidId)
            {
                return BadRequest();
            }

            await _bidRepository.UpdateBidAsync(bid);

            return NoContent();
        }

        // POST: api/Bids
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin, Creator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Bid>> PostBid(Bid bid)
        {
            if (bid == null)
            {
                return BadRequest();
            }

            await _bidRepository.CreateBidAsync(bid);

            return CreatedAtAction("GetBid", new { id = bid.BidId }, bid);
        }

        // DELETE: api/Bids/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBid(int id)
        {
            var result = await _bidRepository.DeleteBidAsync(id);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
