using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITAssetsManagement.Models;

namespace ITAssetsManagement.Services
{
    public static class AssetService
    {
        public static async Task<IList<Asset_Category>> GetAsset_Category()
        {
            IList<Asset_Category> assetCategories;
            using (var context = new AssetContext())
            {
                assetCategories = await context.Asset_Category.ToListAsync();
            }

            return assetCategories;

        }

        public static async Task<IList<Asset_Category>> GetAsset_Category(Expression<Func<Asset_Category, bool>> predicate)
        {
            IList<Asset_Category> assetCategories;

            using (var context = new AssetContext())
            {
                assetCategories = await context.Asset_Category.Where(predicate).ToListAsync();
            }
            return assetCategories;

        }

        public static async Task<Asset_Category> GetAsset_CategoryBy(Expression<Func<Asset_Category, bool>> predicate)
        {
            Asset_Category assetCategory;

            using (var context = new AssetContext())
            {
                assetCategory = await context.Asset_Category.FirstOrDefaultAsync(predicate);
            }
            return assetCategory;

        }

        public static async Task<IList<RMD_Asset_Header>> GetRmd_Asset_Header()
        {
            IList<RMD_Asset_Header> rmdAssetHeaders;
            using (var context = new AssetContext())
            {
                rmdAssetHeaders = await context.RMD_Asset_Header.ToListAsync();
            }

            return rmdAssetHeaders;


        }

        public static async Task<IList<RMD_Asset_Header>> GetRmd_Asset_Header(Expression<Func<RMD_Asset_Header, bool>> predicate)
        {
            IList<RMD_Asset_Header> rmdAssetHeaders;

            using (var context = new AssetContext())
            {
                rmdAssetHeaders = await context.RMD_Asset_Header.Where(predicate).ToListAsync();
            }
            return rmdAssetHeaders;

        }

        public static async Task<RMD_Asset_Header> GetRmd_Asset_HeaderBy(Expression<Func<RMD_Asset_Header, bool>> predicate)
        {
            RMD_Asset_Header rmdAssetHeader;

            using (var context = new AssetContext())
            {
                rmdAssetHeader = await context.RMD_Asset_Header.FirstOrDefaultAsync(predicate);
            }
            return rmdAssetHeader;

        }
    }
}
