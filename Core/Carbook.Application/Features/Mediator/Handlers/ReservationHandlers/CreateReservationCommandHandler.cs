using Carbook.Application.Features.Mediator.Commands.ReservationCommands;
using Carbook.Application.Interfaces;
using CarBook.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbook.Application.Features.Mediator.Handlers.ReservationHandlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand>
    {
        private readonly IRepository<Reservation> _repository;
        public CreateReservationCommandHandler(IRepository<Reservation> repository)
        {
            _repository = repository;
        }
        public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            await _repository.CreateAsync(new Reservation
            {
                //hadi düzelt bu hatayıburdada nullable düzeltmesi yapcaz knk heee anladımm :D ama sorunun şu sen reservation sayfasını açıyon araba kiralamak istiyon hangi araba olduğu belli değil sayfadan viewbagden car id alıyon onu hidden inputa set ediyon ama değeri null değeri yok onu halletmen lazım şimdilik farazi dbden bi car id ekleyelim json dataya sonrasında sen sayfa açılınca hangi arabanın kiralaması yapılacaksa o id yi hidden tutup apiye göndermen lazım
                // okeyok varmı başka birşey kk senin yaptıgın
                // finito anladım knk çok sağol valla hadi bakam kolay gelsin görşürüz sanada knk :EyV)q
                Age = request.Age,
                CarID = request.CarID.Value,
                Description = request.Description,
                DriverLicenseYear = request.DriverLicenseYear,
                DropOffLocationID = request.DropOffLocationID,
                Email = request.Email,
                Name = request.Name,
                Phone = request.Phone,
                PickUpLocationID = request.PickUpLocationID,
                Surname = request.Surname,
                Status = "Rezervasyon Alındı"
            });
        }
    }
}