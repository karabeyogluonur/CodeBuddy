using CB.Application.Abstractions.Services.Features;
using CB.Application.Abstractions.Services.Media;
using CB.Application.Models.Admin.Features;
using CB.Domain.Entities.Features;
using Microsoft.AspNetCore.Mvc;
using CB.Domain.Enums.Media;
using AutoMapper;
using CB.Application.Abstractions.Services.Html;

namespace CB.Web.Areas.Admin.Controllers
{
    public class TalentController : BaseAdminController
    {
        private readonly ITalentService _talentService;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IHtmlNotificationService _htmlNotificationService;

        public TalentController(ITalentService talentService, IFileService fileService, IMapper mapper, IHtmlNotificationService htmlNotificationService)
        {
            _talentService = talentService;
            _fileService = fileService;
            _mapper = mapper;
            _htmlNotificationService = htmlNotificationService;
        }

        public async Task<IActionResult> List()
        {
           List<Talent> talents =  await _talentService.GetAllAsync(showDeactived:true);
           List<TalentListModel> talentListModel = _mapper.Map<List<TalentListModel>>(talents);
           return View(talentListModel);
        }
        public async Task<IActionResult> Create()
        {       
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TalentCreateModel talentCreateModel)
        {
            if (!ModelState.IsValid)
                return View();

            var talentImageInfo = await _fileService.UploadAsync(talentCreateModel.ImageFile, talentCreateModel.Name, RegisteredFileType.TalentImage);
            Talent talent = _mapper.Map<Talent>(talentCreateModel);
            talent.ImageName = talentImageInfo.fileName;
            await _talentService.CreateAsync(talent);

            _htmlNotificationService.SuccessNotification("Yetenek başarıyla oluşturuldu");
            return RedirectToAction("List","Talent");

        }
        public async Task<IActionResult> Update(int id)
        {
            Talent talent = await _talentService.GetByIdAsync(id);
            TalentUpdateModel talentUpdateModel = _mapper.Map<TalentUpdateModel>(talent);
            return View(talentUpdateModel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TalentUpdateModel talentUpdateModel)
        {
            if (!ModelState.IsValid)
                return View();

            Talent talent = await _talentService.GetByIdAsync(talentUpdateModel.Id);
            
            if(talent == null || talent.Deleted)
            {
                _htmlNotificationService.ErrorNotification("Yetenek bulunamadı!");
                return RedirectToAction("List", "Talent");
            }

            _mapper.Map(talentUpdateModel, talent);

            if (talentUpdateModel.ChangeImage)
            {
                var talentImageInfo = await _fileService.UploadAsync(talentUpdateModel.ImageFile, talentUpdateModel.Name, RegisteredFileType.TalentImage);
                talent.ImageName = talentImageInfo.fileName;
            }
             _talentService.Update(talent);
            _htmlNotificationService.SuccessNotification("Yetenek başarıyla güncellendi.");
            return RedirectToAction("List", "Talent");

        }

        public async Task<IActionResult> Delete(int id)
        {
            Talent talent = await _talentService.GetByIdAsync(id);
            if(talent == null || talent.Deleted)
            {
                _htmlNotificationService.ErrorNotification("Yenetek bulunamadı!");
                return RedirectToAction("List", "Talent");
            }
                
            _talentService.Delete(talent);
            _htmlNotificationService.SuccessNotification("Yetenek başarıyla silindi.");

            return RedirectToAction("List", "Talent");

        }
    }
}
