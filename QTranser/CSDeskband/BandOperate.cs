///------------------------------------------------------------------------------
/// @ Y_Theta
///------------------------------------------------------------------------------
using CSDeskBand.Interop;
using System;
using System.Runtime.InteropServices;
using System.Security;


namespace CSDeskBand
{
    public class BandOperate
    {

        #region Methods
        [SecurityCritical()]
        public static void ShowBand(Type t)
        {
            ITrayDeskband csdeskband = null;
            try
            {
                Type trayDeskbandType = Type.GetTypeFromCLSID(new Guid("E6442437-6C68-4f52-94DD-2CFED267EFB9"));
         
                //Guid deskbandGuid = t.GUID;
                Guid deskbandGuid = new Guid("AA01ACB3-6CCC-497C-9CE6-9211F2EDFC10");
                csdeskband = (ITrayDeskband)Activator.CreateInstance(trayDeskbandType);
                if (csdeskband != null)
                {
                    csdeskband.DeskBandRegistrationChanged();
                    if (csdeskband.IsDeskBandShown(ref deskbandGuid) == HRESULT.S_FALSE)
                    {
                        csdeskband.ShowDeskBand(ref deskbandGuid);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while trying to show deskband: {e.ToString()}");
            }
            finally
            {
                if (csdeskband != null && Marshal.IsComObject(csdeskband))
                {
                    Marshal.ReleaseComObject(csdeskband);
                }
            }
        }

        [SecurityCritical()]
        public static void HideBand(Type t)
        {
            ITrayDeskband csdeskband = null;
            try
            {
                Type trayDeskbandType = Type.GetTypeFromCLSID(new Guid("E6442437-6C68-4f52-94DD-2CFED267EFB9"));
                //Guid deskbandGuid = t.GUID;
                Guid deskbandGuid = new Guid("AA01ACB3-6CCC-497C-9CE6-9211F2EDFC10");
                csdeskband = (ITrayDeskband)Activator.CreateInstance(trayDeskbandType);
                if (csdeskband != null)
                {
                    csdeskband.DeskBandRegistrationChanged();
                    if (csdeskband.IsDeskBandShown(ref deskbandGuid) == HRESULT.S_OK)
                    {
                        csdeskband.HideDeskBand(ref deskbandGuid);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while trying to show deskband: {e.ToString()}");
            }
            finally
            {
                if (csdeskband != null && Marshal.IsComObject(csdeskband))
                {
                    Marshal.ReleaseComObject(csdeskband);
                }
            }
        }
        #endregion
    }
}
