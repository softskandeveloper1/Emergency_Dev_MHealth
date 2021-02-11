using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.IService;
using DAL.Models;
using DAL.Utils;
using MHealth.Data.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MHealth.Controllers.api
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceCostController : ControllerBase
    {
        private readonly IServiceCostService _serviceCostService;
        private readonly IProviderCategoryService _providerCategoryService;

        public ServiceCostController(IServiceCostService serviceCostService, IProviderCategoryService providerCategoryService)
        {
            _serviceCostService = serviceCostService;
            _providerCategoryService = providerCategoryService;
        }

        public IActionResult GetClinicianServiceCost(int service_id,Guid clinician_id,int appointment_activity_sub_id)
        {
            return Ok(_serviceCostService.Get().FirstOrDefault(e => e.clinician_id == clinician_id && e.appointment_service_id == service_id && e.appointment_activity_sub_id == appointment_activity_sub_id));
        }

        public IActionResult GetClinicianServiceCosts(Guid clinician_id)
        {
            var costs = _serviceCostService.Get().Where(e => e.clinician_id == clinician_id);
            var costing_models = new List<CostingModel>();

            var provider_categories = _providerCategoryService.GetClinicianCategory().Where(e => e.clinician_id == clinician_id).Include(e => e.appointment_category_subNavigation);

            foreach (var provider_category in provider_categories)
            {
                var appointment_sub_services = Options.GetAppointmentSubServices().Where(e => e.activity_sub_id == provider_category.appointment_category_sub).Select(e => e.appointment_service_id);

                var appointment_services = Options.GetAppointmentServices().Where(e => appointment_sub_services.Contains(e.id));

                foreach (var appointment_service in appointment_services)
                {
                    decimal amount = 0;
                    var service_cost=costs.FirstOrDefault(e => e.appointment_activity_sub_id == provider_category.appointment_category_sub && e.appointment_service_id == appointment_service.id);
                    if (service_cost != null)
                    {
                        amount = service_cost.cost;
                    }

                    costing_models.Add(new CostingModel { service_id = appointment_service.id, service_name = appointment_service.name, sub_id = provider_category.appointment_category_sub, sub_name = provider_category.appointment_category_subNavigation.name, cost = amount });
                }
            }

            return Ok(costing_models);
        }

        [HttpPost]
        public IActionResult Post(ServiceCostModel model)
        {

            for (var i = 0; i < model.CostingModels.Count; i++)
            {
                var service_cost = new mp_service_costing
                {
                    //appointment_type_id = 1,
                    clinician_id = model.clinician_id,
                    created_by = model.created_by
                };

                var cost = model.CostingModels[i].cost;
                var appointment_activity_sub = model.CostingModels[i].sub_id;
                var service_id = model.CostingModels[i].service_id; ; //Convert.ToInt32(service_ids[i]);
                if (cost > 0)
                {
                    service_cost.cost = Convert.ToDecimal(cost);
                    service_cost.appointment_service_id = service_id;
                    service_cost.appointment_activity_sub_id = appointment_activity_sub;

                    _serviceCostService.AddOrUpdate(service_cost);
                }
            }

            return Ok(200);
        }
    }
}