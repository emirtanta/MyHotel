﻿using HotelProject.BusinessLayer.Abstract;
using HotelProject.EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly ISendMessageService _sendMessageService;

        public SendMessageController(ISendMessageService sendMessageService)
        {
            _sendMessageService = sendMessageService;
        }

        [HttpGet]
        public IActionResult SendMessageList()
        {
            var values = _sendMessageService.TGetList();

            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetSendMessage(int id)
        {
            var values = _sendMessageService.TGetByID(id);


            return Ok(values);
        }

        [HttpPost]
        public IActionResult AddSendMessage(SendMessage sendMessage)
        {
            _sendMessageService.TInsert(sendMessage);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSendMessage(int id)
        {
            var values = _sendMessageService.TGetByID(id);
            _sendMessageService.TDelete(values);

            return Ok();
        }



        [HttpPut]
        public IActionResult UpdateSendMessage(SendMessage sendMessage)
        {
            _sendMessageService.TUpdate(sendMessage);

            return Ok();
        }

        [HttpGet("GetSendMessageCount")]
        public IActionResult GetSendMessageCount()
        {
            return Ok(_sendMessageService.TGetSendMessageCount());
        }
    }
}
