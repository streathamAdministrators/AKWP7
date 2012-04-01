using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Data;
using RMM.Data.Model;
using Microsoft.Phone.Shell;

namespace RMM.Business.OptionService
{
    public class OptionService : IOptionService
    {
        private RmmDataContext datacontext = null;

        public Result<Option> GetOption()
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var option = (from t in datacontext.Option
                                  select t).First();

                    result.Value = option;
                }

            }, () => "erreur");
        }

        public Result<Option> UpdateOption(EditOptionCommand optionToUpdate)
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var entityToUpdate = datacontext.Option.First();

                    entityToUpdate.IsPassword = optionToUpdate.IsPassword;
                    entityToUpdate.IsPrimaryTile = optionToUpdate.IsPrimaryTile;
                    entityToUpdate.IsSynchro = optionToUpdate.IsSynchro;
                    entityToUpdate.Favorite = optionToUpdate.Favorite;
                    entityToUpdate.ModifiedDate = DateTime.Now;

                    datacontext.SubmitChanges();

                    result.Value = entityToUpdate;
                }

            }, () => "erreur");
        }


        public Result<Option> SetFirstTimeOption()
        {
            return Result<Option>.SafeExecute<IOptionService>(result =>
            {
                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var newFirstTimeOption = new Option()
                    {
                        id = 1,
                        IsPrimaryTile = false,
                        IsPassword = false,
                        IsSynchro = false,
                        Favorite = 0,
                        ModifiedDate = DateTime.Now
                    };

                    datacontext.Option.InsertOnSubmit(newFirstTimeOption);

                    datacontext.SubmitChanges();

                    result.Value = newFirstTimeOption;
                }
            }, () => "erreur");


        }


        public Result<int> GetFavoriteIdAccount()
        {

            return Result<int>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {
                    var idFavorite = (from t in datacontext.Option
                                      select t.Favorite).First();

                    result.Value = idFavorite;
                }

            }, () => "erreur");

        }


        public Result<int> SetFavoriteIdAccount(int AccountId)
        {
            return Result<int>.SafeExecute<IOptionService>(result =>
            {

                using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                {

                    var entityToUpdate = datacontext.Option.First();

                    entityToUpdate.Favorite = AccountId;

                    

                    datacontext.SubmitChanges();

                    result.Value = AccountId;
                }

            }, () => "erreur");
        }


        #region PrimaryTile

        public Result<bool?> IsPrimaryTileOptionEnable()
        {
            return Result<bool?>.SafeExecute<IOptionService>(result =>
                {
                    using (datacontext = new RmmDataContext(RmmDataContext.CONNECTIONSTRING))
                    {
                        var isPrimaryTile = (from t in datacontext.Option
                                             select t.IsPrimaryTile).First();

                        result.Value = isPrimaryTile;
                    }
                }, () => "erreur");
        }

        public Result<bool> SetPrimaryTileInfo(string favoriteAccountName, string favoriteBankName, double favoriteAmount)
        {
            return Result<bool>.SafeExecute<IOptionService>(result =>
                {
                    ShellTile primaryTile = ShellTile.ActiveTiles.First();
                    if (primaryTile != null)
                    {
                        StandardTileData tileData = new StandardTileData();
                        tileData.BackTitle = favoriteBankName;
                        string stringFavoriteAmount = favoriteAmount.ToString();
                        tileData.BackContent = favoriteAccountName + "\n" + stringFavoriteAmount;
                        primaryTile.Update(tileData);
                    }
                }, () => "erreur");
        }

        public Result<bool> DisablePrimaryTile()
        {
            return Result<bool>.SafeExecute<IOptionService>(result =>
                {
                    ShellTile primaryTile = ShellTile.ActiveTiles.First();
                    if (primaryTile != null)
                    {
                        StandardTileData tileData = new StandardTileData();
                        tileData.BackTitle = string.Empty;
                        tileData.BackContent = string.Empty;
                        primaryTile.Update(tileData);
                        //primaryTile.Delete();
                    }
                }, () => "erreur");
        }


        #endregion
    }
}
