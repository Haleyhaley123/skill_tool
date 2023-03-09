using Foundatio.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTS.Common.Utils
{
    public static class CacheUtil
    {
        /// <summary>
        /// Xóa cache liên quan đến đối tượng
        /// </summary>
        /// <param name="cacheClient"></param>
        public async static void ClearObject(ICacheClient cacheClient)
        {
            try
            {
                //Clear cache Địa bàn
                var prefixKey = $"{ NTSConstants.PrefixSystemKey }{NTSConstants.PrefixKey_DiaBan}";
                await cacheClient.RemoveByPrefixAsync(prefixKey);

                prefixKey = $"{ NTSConstants.PrefixSystemKey }{NTSConstants.PrefixKey_DiaBanNgBien}";
                await cacheClient.RemoveByPrefixAsync(prefixKey);
            }
            catch (Exception ex) {  }
        }

        /// <summary>
        /// Xóa cache theo địa bàn
        /// </summary>
        /// <param name="cacheClient"></param>
        public async static void ClearDiaBan(ICacheClient cacheClient,string idDiaBan)
        {
            try
            {
                //Clear cache Địa bàn
                var key = $"{ NTSConstants.PrefixSystemKey }{NTSConstants.PrefixKey_DiaBan}{idDiaBan}";

                await cacheClient.RemoveAsync(key);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Xóa cache theo địa bàn liên quan
        /// </summary>
        /// <param name="cacheClient"></param>
        public async static void ClearDiaBanLienQuan(ICacheClient cacheClient, string idDiaBanLienQuan)
        {
            try
            {
                //Clear cache Địa bàn
                var key = $"{NTSConstants.PrefixSystemKey}{NTSConstants.PrefixKey_DiaBanLienQuan}{idDiaBanLienQuan}";

                await cacheClient.RemoveAsync(key);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Xóa cache theo địa bàn ngoại biên
        /// </summary>
        /// <param name="cacheClient"></param>
        public async static void ClearDiaBanNgB(ICacheClient cacheClient, string idDiaBanNB)
        {
            try
            {
                //Clear cache Địa bàn
                var key = $"{NTSConstants.PrefixSystemKey}{NTSConstants.PrefixKey_DiaBanNgBien}{idDiaBanNB}";

                await cacheClient.RemoveAsync(key);
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// Xóa cache theo tuyến
        /// </summary>
        /// <param name="cacheClient"></param>
        public async static void ClearTuyen(ICacheClient cacheClient, string idTuyen)
        {
            try
            {
                //Clear cache Địa bàn
                var key = $"{NTSConstants.PrefixSystemKey}{NTSConstants.PrefixKey_Tuyen}{idTuyen}";

                await cacheClient.RemoveAsync(key);
            }
            catch (Exception ex) { }
        }
    }
}
