﻿using Microsoft.AspNetCore.Mvc;
using TatBlog.services.Blogs;

namespace TatBlog.WebApp.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly IMailservice _mailService;
        private readonly ISubscriberRepository _subscriberRepository;

        public NewsletterController(IMailservice mailService, ISubscriberRepository subscriberRepository)
        {
            _mailService = mailService;
            _subscriberRepository = subscriberRepository;
        }
        public IActionResult Index()
        {
            return View("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            await _subscriberRepository.SubscribeAsync(email);
            return RedirectToAction(actionName: "Index", controllerName: "Blog", new { area = "" });
        }
        [HttpGet]
        public async Task<IActionResult> Unsubscribe([FromQuery] string email)
        {
            await _subscriberRepository.UnsubscribeAsync(email, "không muốn đăng kí nữa");
            return RedirectToAction(actionName: "Index", controllerName: "Blog", new { area = "" });
        }
    }
}
