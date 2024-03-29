﻿using Homies.Contracts;
using Homies.Extensions;
using Homies.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homies.Controllers
{
    public class EventController : BaseController
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<AllEventViewModel> allEvents =
                await this.eventService.GetAllEventsAsync();

            return View(allEvents);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AddEventViewModel viewModel = await this.eventService.GetAddEventModelAsync();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model = await eventService.GetAddEventModelAsync();
                return View(model);
            }

            try
            {
                var userId = this.User.GetId();

                await this.eventService.AddEventAsync(model, userId);
            }
            catch (InvalidDataException e)
            {
                model = await this.eventService.GetAddEventModelAsync();

                ModelState.AddModelError("", e.Message);

                return View(model);
            }

            return RedirectToAction("All", "Event");
        }

        public async Task<IActionResult> Join(string id)
        {
            try
            {
                var userId = this.User.GetId();

                await this.eventService.JoinEventAsync(int.Parse(id), userId);

                return RedirectToAction("Joined", "Event");
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Event");
            }
        }

        public async Task<IActionResult> Joined()
        {
            var userId = this.User.GetId();

            var events = await this.eventService.GetJoinedEventsAsync(userId);

            return View(events);
        }

        public async Task<IActionResult> Leave(string id)
        {
            if (int.TryParse(id, out int eventId))
            {
                var userId = this.User.GetId();

                await this.eventService.LeaveEventAsync(eventId, userId);

                return RedirectToAction("All", "Event");
            }

            return RedirectToAction("Joined", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (int.TryParse(id, out int eventId))
            { 
                var model = await this.eventService.GetEditAsync(eventId);
                model.Id = eventId;

                return View(model);
            }

            return RedirectToAction("All", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Types = await this.eventService.GetEventTypesAsync();

                ModelState.AddModelError("", "Invalid data!");

                return View(model);
            }

            try
            {
                await this.eventService.UpdateEventAsync(model);

                return RedirectToAction("All", "Event");
            }
            catch (Exception)
            {
                model.Types = await this.eventService.GetEventTypesAsync();

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var model = await this.eventService.GetEventDetailsAsync(int.Parse(id));

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("All", "Event");
            }
        }
    }
}
