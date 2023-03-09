using NTS.Common.Models;
using System.Threading.Tasks;

namespace Training.Services.Signalr
{
    public interface IHubClient
    {
        //Task BroadcastMessage(MessageInstance msg);
        Task DashBoard();

        Task ReloadChuyenAn();

        Task ReloadDoiTuongViPhamHC();

        Task ReloadDoiTuongDTHS();

        Task ReloadNguoiNghien();
    }
}