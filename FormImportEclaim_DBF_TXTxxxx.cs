using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System.Threading;
using OPBKKClaim.MyClass.Model;



namespace OPBKKClaim
{
    public partial class FormImportEclaim_DBF_TXT : DevComponents.DotNetBar.Metro.MetroForm
    {
		//=================================
        public MyClass.Delegate.MyDelegate.DImportProgressChanged dImportProgressChanged;
        public MyClass.Delegate.MyDelegate.DImportFinished dImportFinished;

		//=================================
		// mai: OK
		// mai: use "_import" to make "btnImport" as toggle function, press once to start/stop "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()", this var also use to stop thread of "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()"!!!
		// mai: set to "true" before start thread of "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()" to tell that we are importing-DBF/Text, and set to "false" to reset when "importFinished()".
		public bool IsImporting { get; set; } = false;	// mai: init with "false"	// mai: OK

		//=================================
        private RegisterManager registerManager = null; // mai: init with "null"	// mai: OK

		//=================================
		private int fileTypes { get; set; } = Import_Const.FileTypes.NULL;	// mai: init with "Import_Const.FileTypes.NULL"	// mai: OK // OK

		//=================================
		private bool isByPassConfirmClosingDialog = false; // mai: init with "false"	// mai: OK

		//=================================



		public FormImportEclaim_DBF_TXT(int pFileType, string labxBrowseFolder_Text)	// OK-ROUGH
        {
            InitializeComponent();

			//=================================
            dImportProgressChanged = new MyClass.Delegate.MyDelegate.DImportProgressChanged(this.importProgressChanged);
            dImportFinished = new MyClass.Delegate.MyDelegate.DImportFinished(this.importFinished);

			//=================================
			this.labxBrowseFolder.Text = labxBrowseFolder_Text;

			//=================================
			fileTypes = pFileType;

			//=================================
        } // ctor



		private void FormImportEclaim_DBF_TXT_FormClosing(object sender, FormClosingEventArgs e)	// OK5-ROUGH
		{
			if (registerManager != null)
			{
registerManager.IsWaitClose = true;						// mai: to pause RegisterManager-Importing-Process.

				if (!isByPassConfirmClosingDialog)		// mai: check if Bypass this ConfirmClosingDialog when finished all jobs.
				{
					// mai: ask user to confirm for closing this form, in this case if user click on btn-NO to NOT-Closing this form.
					DialogResult dialogResult = FormMyMessageBox.Show("��ҹ��ͧ���¡��ԡ��ù���Ң��������������", MyConst.APP_NAME, MessageBoxButtons.YesNo);	// mai	// OK // Ok
					if (dialogResult == DialogResult.No)    // mai	// WK
					{
						e.Cancel = true;	// mai: Cancel the Closing-Event from closing the form.

registerManager.IsWaitClose = false;	// reset		// mai: to continue RegisterManager-Importing-Process.
						return;
					} // if (dialogResult == DialogResult.No)
				} // if (!isByPassConfirmClosingDialog)

registerManager.IsClose = true;
			} // if (registerManager != null)

			//---------------------------------
ImportLog.deleteImportLog();	// mai added
			this.DialogResult = DialogResult.OK;	// mai added why why 
		} // FormImportEclaim_DBF_TXT_FormClosing

        private void btnxClose_Click(object sender, EventArgs e)	// OK5
        {
            this.Close();	// will calling "FormClosing()"
        } // btnxClose_Click

        private void btnxBrowseFolder_Click(object sender, EventArgs e)	// OK-ROUGH
        {
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();  // org
			//OpenFileDialog openFileDialog = new OpenFileDialog();    // mai
			//=================================
			if (fileTypes == Import_Const.FileTypes.DBF)
            {
				string msg = "��س����͡������������� DBF" ;
				folderBrowserDialog.Description = msg;
				//openFileDialog.Title = msg;  // OpenFileDialog
			}
            else if (fileTypes == Import_Const.FileTypes.TXT)
            {
				string msg = "��س����͡������������� Text";
				folderBrowserDialog.Description = msg;
				//openFileDialog.Title = msg; // OpenFileDialog
			}
			//=================================
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
				txtBxxBrowseFolder.Text = folderBrowserDialog.SelectedPath;
				//---------------------------------
lblxMsg.Text = "��سҡ���������Ң��������ʹ��Թ���";
btnxImport.Enabled = true;
				//---------------------------------
            }
			//=================================
        } // btnxBrowseFolder_Click

		private void btnxImport_Click(object sender, EventArgs e)	// OK5
		{
			if (!IsImporting)	// mai: OK
			{
// mai: Start "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()":

				//==================================================================
ImportLog.deleteImportLog();

				//==================================================================
				bool updatePendingApprovedTransaction = false;	// mai: init with "false"
				if (RegisterManager.chkPendingApprovedTransaction())	// OK
				{
					using (FormAlertUpdatePendingApprovedTransaction formAlertUpdatePendingApprovedTransaction = new FormAlertUpdatePendingApprovedTransaction())
					{
						DialogResult dialogResult = formAlertUpdatePendingApprovedTransaction.ShowDialog();
						if (dialogResult == DialogResult.OK)
						{
							updatePendingApprovedTransaction = formAlertUpdatePendingApprovedTransaction.UpdatePendingApprovedTransaction;	// mai: values preserved after close!
						}
					}
				} // if (RegisterManager.chkPendingApprovedTransaction())
				//---------------------------------
				if (updatePendingApprovedTransaction)
				{
IsImporting = false;	// mai: reset
					this.Close();	// will calling "FormClosing()"
return;
                }

				//==================================================================
                if (fileTypes == Import_Const.FileTypes.DBF)
					lblxMsg.Text = $@"���ѧ���Թ��ù���Ң����Ũҡ��� DBF";
                else if (fileTypes == Import_Const.FileTypes.TXT)
					lblxMsg.Text = $@"���ѧ���Թ��ù���Ң����Ũҡ��� TXT";

				pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = true;
				//---------------------------------
				btnxBrowseFolder.Enabled = false;
				//---------------------------------
				btnxImport.Text = $@"¡��ԡ";
				btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;
				//---------------------------------
				btnxExportLog.Enabled = false;

				//==================================================================
IsImporting = true;	// mai: telling that going to do "registerManager.ImportDBF()/ImportText()" and telling that thread is running for importing and use to stop thread when press import-btn again, before start thread set this to true!!!

				//------------------------------------------------------------------
// mai: share with "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()--btnxImport_Click()" and "_frmImportSummary.RegisterManager--importFinished()"
registerManager = new RegisterManager();	// Clear old value		// org	// mai: reset by create new instance.	// mai: OK
				registerManager.CallBackForm = this;	// mai: use to call member of this Form from other Form, e.g. _frmImportEclaim.Import!!!
				registerManager.FolderPath = txtBxxBrowseFolder.Text;

				if (fileTypes == Import_Const.FileTypes.DBF)
					new Thread(new ThreadStart(registerManager.importDBF_Phase1)).Start();
				else if (fileTypes == Import_Const.FileTypes.TXT)
					new Thread(new ThreadStart(registerManager.importTXT_Phase1)).Start();
			} // if (!IsImporting)
			else
			{
// mai: Cancel thread "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()" with "IsImporting = false":

				// mai: added:
				//DialogResult dialogResult = FormMyMessageBox.Show("��ҹ��ͧ���¡��ԡ��ù���Ң��������������", MyConst.APP_NAME, MessageBoxButtons.YesNo);
				//if (dialogResult == DialogResult.Yes)
				{
					//==================================================================
					lblxMsg.Text = $@"���ѧ¡��ԡ��ù���Ң�����";
					//pgbImport.Value = 0;
					//------------------------------------------------------------------
					picBxLoading.Visible = true;
					//---------------------------------
					btnxBrowseFolder.Enabled = false;
					//---------------------------------
					btnxImport.Text = $@"����Ң�����";
					btnxImport.Enabled = false;
					//---------------------------------
					btnxClose.Enabled = false;
					//---------------------------------
					btnxExportLog.Enabled = false;

					//==================================================================
IsImporting = false;	// mai: reset, and also use this var to stop thread of "registerManager.ImportDBF_Phase1()/ImportTXT_Phase1()".
				} // if--dialogResult-confirm-cancel
			} // else // if (!IsImporting)
		} // btnxImport_Click

        private void btnxExportLog_Click(object sender, EventArgs e)	// OK-ROUGH
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "csv files (*.csv)|*.csv";
				DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    CSVImportLogger.DeleteImportLogFile(saveFileDialog.FileName);
                    CSVImportLogger.CreateImportLogFile(saveFileDialog.FileName, registerManager.LstCSVImportLogData);

                    FormMyMessageBox.Show("�ѹ�֡���������º��������", MyConst.APP_NAME, MessageBoxButtons.OK);
                }
            } // try
            catch (Exception ex)
            {
                Logger.Error(ex);
                FormMyMessageBox.Show("Error: " + ex.Message, MyConst.APP_NAME, MessageBoxButtons.OK);	// mai
            } // catch
        } // btnxExportLog_Click

        private void importProgressChanged(string prgMsg, string prgVal)	// OK5
        {
			lblxMsg.Text = prgMsg != null ? prgMsg : "";
			pgbImport.Value = prgVal != null ? MyUtil.toInt(prgVal) : 0;
        } // importProgressChanged

		private void importFinished()	// OK5
		{
			//==================================================================
// mai: if any error occurs/user Cancel thread, thus show message and reset "IsImporting" with "false" and return to get out of this method:

			if (registerManager.IsOver12Files)	// OK5
			{
				//==================================================================
				if (fileTypes == Import_Const.FileTypes.DBF)
				{
					FormErrorShortMsg _form = new FormErrorShortMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"����� DBF �ҡ���� 12 ���"
					);
				}
				else if (fileTypes == Import_Const.FileTypes.TXT)
				{
					FormErrorShortMsg _form = new FormErrorShortMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"����� TXT �ҡ���� 12 ���"
					);
				}
				//------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = $@"����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
                IsImporting = false;	// mai: reset
                return;
			} // if (registerManager.IsOver12Files)

			if (registerManager.IsFilesDuplicated)	// OK5
			{
				//==================================================================
				if (fileTypes == Import_Const.FileTypes.DBF)
				{
					FormErrorLongMsg _form = new FormErrorLongMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"����� DBF ��� �ѧ���仹��: " + Environment.NewLine
						+ registerManager.StrFilesDuplicated__Log
					);
				}
				else if (fileTypes == Import_Const.FileTypes.TXT)
				{
					FormErrorLongMsg _form = new FormErrorLongMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"����� TXT ��� �ѧ���仹��: " + Environment.NewLine
						+ registerManager.StrFilesDuplicated__Log
					);
				}
				//------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	//
return;
			} // if (registerManager.IsFilesDuplicated)

    		if (!registerManager.TXT__IsEqualityLenghtOfFieldNameAndFieldData)	// OK5
			{
				//==================================================================
				FormErrorLongMsg _form = new FormErrorLongMsg();
				DialogResult dialogResult = _form.CustomShowDialog_ret(
					$@"��ù���Ң����żԴ��Ҵ!"
					,
					$@"�դ����Դ��Ҵ����ǡѺ�ӹǹ��������������ҡѺ�ӹǹ�ͧ���Ϳ�Ŵ� ����ѧ���仹�� A:" + Environment.NewLine
					+ $@"{registerManager.Str_TXT__InEqualityLenghtOfFieldNameAndFieldData__Log}"
				);
				//------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = true;	// YAYA ����� false
return;
			} /// if (!registerManager.TXT__IsEqualityLenghtOfFieldNameAndFieldData)

			if (!registerManager.IsSuccessGettingDataFromFilesToDataTablesVars)	// OK5
			{
				//==================================================================
				if (fileTypes == Import_Const.FileTypes.DBF)
				{
					FormErrorLongMsg _form = new FormErrorLongMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"��辺��� DBF �ѧ���仹��: " + Environment.NewLine
						+ registerManager.StrFilesInProcess
					);
				}
				else if (fileTypes == Import_Const.FileTypes.TXT)
				{
					FormErrorLongMsg _form = new FormErrorLongMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"��辺��� TXT �ѧ���仹��: " + Environment.NewLine
						+ registerManager.StrFilesInProcess
					);
				}
				//------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
                IsImporting = false;	// mai: reset
                return;
                //--------------------------
			} // if (!registerManager.IsSuccessGettingDataFromFilesToDataTablesVars)

          /*  if (!registerManager.DBF_TXT__OPD_CLAIM_CODE__HasFieldName) // OK5
            {
                //==================================================================
                if (fileTypes == Import_Const.FileTypes.DBF)
                {
                    FormErrorShortMsg _form = new FormErrorShortMsg();
                    DialogResult dialogResult = _form.CustomShowDialog_ret(
                        $@"��ù���Ң����żԴ��Ҵ!"
                        ,
                        //$@"�ô��Ǩ�ͺ�����١��ͧ�ͧ���Ϳ�Ŵ� CLAIM_CODE ���� OPD"	
                        $@"�ô��Ǩ�ͺ�����١��ͧ�ͧ�ç���ҧ�����Ţͧ��������� OPD ��辺��Ŵ� CLAIM_CODE"    //YAYA
                    );
                }
                else if (fileTypes == Import_Const.FileTypes.TXT)
                {
                    FormErrorShortMsg _form = new FormErrorShortMsg();
                    DialogResult dialogResult = _form.CustomShowDialog_ret(
                        $@"��ù���Ң����żԴ��Ҵ!"
                        ,
                        //$@"�ô��Ǩ�ͺ�����١��ͧ�ͧ���Ϳ�Ŵ� CLAIM_CODE ���� OPD"	
                        $@"�ô��Ǩ�ͺ�����١��ͧ�ͧ�ç���ҧ�����Ţͧ��������� OPD ��辺��Ŵ� CLAIM_CODE"    // yaya
                    );
                }  
                //------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
                //------------------------------------------------------------------
                picBxLoading.Visible = false;
                //---------------------------------
                btnxBrowseFolder.Enabled = true;
                //---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
                //---------------------------------
                btnxClose.Enabled = true;

                //==================================================================
                IsImporting = false;	// mai: reset
                return;
            }
*/
                if (!registerManager.DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__HasFieldName)	// OK5
			{
				//==================================================================
				if (fileTypes == Import_Const.FileTypes.DBF)
				{
					FormErrorShortMsg _form = new FormErrorShortMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						//$@"�ô��Ǩ�ͺ�����١��ͧ�ͧ���Ϳ�Ŵ� ADDON_DESC ���� CHAD"	// mai: ver02
						$@"�ô��Ǩ�ͺ�����١��ͧ�ͧ�ç���ҧ�����Ţͧ��������� CHAD ��辺��Ŵ� ADDON_DESC"	// mai: ver03
					);
				}
				else if (fileTypes == Import_Const.FileTypes.TXT)
				{
					FormErrorShortMsg _form = new FormErrorShortMsg();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						//$@"�ô��Ǩ�ͺ�����١��ͧ�ͧ���Ϳ�Ŵ� ADDON_DESC/ADDON_DETAIL ���� CHAD"	// mai: ver02
						$@"�ô��Ǩ�ͺ�����١��ͧ�ͧ�ç���ҧ�����Ţͧ��������� CHAD ��辺��Ŵ� ADDON_DESC"	// mai: ver03
					);
				}
				//------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
                IsImporting = false;	// mai: reset
                return;
			} // if (!registerManager.DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__HasFieldName)

			//if (!registerManager.DBF_TXT_HospitalOS__CHAD__ADDON_DESC__ADDON_DETAIL__IsValidDentistryData)	// OK5
			bool isEnableDentistryHere = false;	// mai: disable, NOT USE HERE NOW.
			if (isEnableDentistryHere)
			{
				//==================================================================
				if (registerManager.ChkDentistry_.firstInvalidRow == -2)	// return -2 == catching some errors.
				{
					FormError__ADDON_DESC__Dentistry _form = new FormError__ADDON_DESC__Dentistry();
					DialogResult dialogResult = _form.CustomShowDialog_ret(
						$@"��ù���Ң����żԴ��Ҵ!"
						,
						$@"{registerManager.ChkDentistry_.strInvalidRow}" + Environment.NewLine
						+ $@"�ô�ٵ�����ҧ����кث��ѹ�����ҹ��ҧ���"
					);
				}
				else	// return positive number of the "firstInvalidRow"
				{
					if (fileTypes == Import_Const.FileTypes.DBF)
					{
						FormError__ADDON_DESC__Dentistry _form = new FormError__ADDON_DESC__Dentistry();
						DialogResult dialogResult = _form.CustomShowDialog_ret(
							$@"��ù���Ң����żԴ��Ҵ!"
							,
							$@"�����ŷѹ������Դ��Ҵ����Ŵ� ADDON_DESC ���� CHAD" + Environment.NewLine
							+ $@"���á���Դ��Ҵ : {registerManager.ChkDentistry_.firstInvalidRow}" + Environment.NewLine
							+ $@"�ӹǹ�Ƿ��Դ��Ҵ������ : {registerManager.ChkDentistry_.totalInvalidRow}" + Environment.NewLine
							+ $@"��������´�����Դ��Ҵ : {registerManager.ChkDentistry_.strInvalidRow}" + Environment.NewLine
							+ $@"�ô�ٵ�����ҧ����кث��ѹ�����ҹ��ҧ���"
						);
					}
					else if (fileTypes == Import_Const.FileTypes.TXT)
					{
						FormError__ADDON_DESC__Dentistry _form = new FormError__ADDON_DESC__Dentistry();
						DialogResult dialogResult = _form.CustomShowDialog_ret(
							$@"��ù���Ң����żԴ��Ҵ!"
							,
							//$@"�����ŷѹ������Դ��Ҵ����Ŵ� ADDON_DESC/ADDON_DETAIL ���� CHAD" + Environment.NewLine
							$@"�����ŷѹ������Դ��Ҵ����Ŵ� ADDON_DESC ���� CHAD" + Environment.NewLine	// mai: new ver NOT SHOW "ADDON_DETAIL"
							+ $@"���á���Դ��Ҵ : {registerManager.ChkDentistry_.firstInvalidRow}" + Environment.NewLine
							+ $@"�ӹǹ�Ƿ��Դ��Ҵ������ : {registerManager.ChkDentistry_.totalInvalidRow}" + Environment.NewLine
							+ $@"��������´�����Դ��Ҵ : {registerManager.ChkDentistry_.strInvalidRow}" + Environment.NewLine
							+ $@"�ô�ٵ�����ҧ����кث��ѹ�����ҹ��ҧ���"
						);
					}
					else if (fileTypes == Import_Const.FileTypes.HospitalOS)
					{
						FormError__ADDON_DESC__Dentistry _form = new FormError__ADDON_DESC__Dentistry();
						DialogResult dialogResult = _form.CustomShowDialog_ret(
							$@"��ù���Ң����żԴ��Ҵ!"
							,
							$@"�����ŷѹ������Դ��Ҵ����Ŵ� ADDON_DESC 㹵��ҧ CHAD" + Environment.NewLine
							+ $@"���á���Դ��Ҵ : {registerManager.ChkDentistry_.firstInvalidRow}" + Environment.NewLine
							+ $@"�ӹǹ�Ƿ��Դ��Ҵ������ : {registerManager.ChkDentistry_.totalInvalidRow}" + Environment.NewLine
							+ $@"��������´�����Դ��Ҵ : {registerManager.ChkDentistry_.strInvalidRow}" + Environment.NewLine
							+ $@"�ô�ٵ�����ҧ����кث��ѹ�����ҹ��ҧ���"
						);
					}
				} // else // if (registerManager.ChkDentistry_.firstInvalidRow == -2)
				//------------------------------------------------------------------
                lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	// mai: reset
return;
			} //if (!registerManager.DBF_TXT_HospitalOS__CHAD__ADDON_DESC__ADDON_DETAIL__IsValidDentistryData)

			// mai: for "subImportXXX_Phase1()"
			//---------------------------------
			if (!registerManager.Has__ImportAllRow__allOPD)	// OK5
			{
				//==================================================================
				FormErrorShortMsg _form = new FormErrorShortMsg();
				DialogResult dialogResult = _form.CustomShowDialog_ret(
					$@"��ù���Ң����żԴ��Ҵ!"
					,
					$@"¡��ԡ��ù���Ң����� ���ͧ�ҡ����բ����š�����Ѻ��ԡ�� (��ҧ�ԧ�ҡ��� OPD)"
				);
				//------------------------------------------------------------------
                //lblxMsg.Text = $@"¡��ԡ��ù���Ң����� ���ͧ�ҡ����բ����š�����Ѻ��ԡ�� (��ҧ�ԧ�ҡ��� OPD)";
				lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
				pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	// mai: reset
return;
			} // if (!registerManager.HaveDataRecordsToProcess__BasedOnOPD)

			if (!IsImporting)	// OK5
			{
				//==================================================================
				// NOTHING TO DO HERE, UPDATE OF user click-btn to cancel thread ALREADY DO IN "subImportXXX_Phase1()"

				//lblxMsg.Text = $@"¡��ԡ��ù���Ң����� �����������º��������";
				//pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	// mai: reset
return;
			} // if (!IsImporting)

			if (!registerManager.IsValidHCODE)	// OK5
			{
				//==================================================================
				FormErrorShortMsg _form = new FormErrorShortMsg();
				DialogResult dialogResult = _form.CustomShowDialog_ret(
					$@"��ù���Ң����żԴ��Ҵ!"
					,
					//$@"����ʶҹ��Һ������������ ���ç�Ѻʶҹ��Һ�ŷ���駤�������к�"
					$@"����ʶҹ��Һ�ŷ������ ���ç�Ѻʶҹ��Һ�ŷ���駤�������к�"
				);
				//------------------------------------------------------------------
				//lblxMsg.Text = $@"¡��ԡ��ù���Ң����� ���ͧ�ҡ HCode ���١��ͧ";   // mai: from org, just keep for may use.
				lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
				pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	// mai: reset
return;
			} // if (!registerManager.IsValidHCODE)

			if (registerManager.Is__subImportXXX_PhaseXXX__CatchError)	// OK5
			{
				//==================================================================
				// NOTHING TO DO HERE, UPDATE OF CATCHING-ERROR of "subImportXXX_PhaseXXX()" ALREADY DO inside its method.

                //lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                //pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	// mai: reset
return;
			} // if (registerManager.Is__subImportXXX_PhaseXXX__CatchError)

			if (registerManager.Is__importXXX_PhaseXXX__CatchError)	// OK5
			{
				//==================================================================
				// NOTHING TO DO HERE, UPDATE OF CATCHING-ERROR of "importXXX_Phase1()" ALREADY DO inside its method.

                //lblxMsg.Text = $@"��ù���Ң����żԴ��Ҵ!";
                //pgbImport.Value = 0;
				//------------------------------------------------------------------
				picBxLoading.Visible = false;
				//---------------------------------
				btnxBrowseFolder.Enabled = true;
				//---------------------------------
                btnxImport.Text = "����Ң�����";
                btnxImport.Enabled = true;
				//---------------------------------
				btnxClose.Enabled = true;

				//==================================================================
IsImporting = false;	// mai: reset
return;
			} // if (registerManager.Is__importXXX_PhaseXXX__CatchError)

			//==================================================================
// mai: There's NO any error occurs/user NOT Cancel thread, thus continue "FormImportSummary":

			//==================================================================
			lblxMsg.Text = $@"����Ң��������º��������hos";
			pgbImport.Value = 100;
			//------------------------------------------------------------------
			picBxLoading.Visible = false;
			//---------------------------------
			btnxBrowseFolder.Enabled = true;
			//---------------------------------
			btnxImport.Text = "����Ң�����";
			btnxImport.Enabled = true;
			//---------------------------------
			btnxClose.Enabled = true;
			//---------------------------------
if (registerManager.LstCSVImportLogData.Count > 0) btnxExportLog.Enabled = true;

			//==================================================================
IsImporting = false;	// mai: reset

			//------------------------------------------------------------------
			FormImportSummary formImportSummary = null;

			if (fileTypes == Import_Const.FileTypes.DBF)
				formImportSummary = new FormImportSummary(SourceTypes.DBF);
			else if (fileTypes == Import_Const.FileTypes.TXT)
				formImportSummary = new FormImportSummary(SourceTypes.TXT);

formImportSummary.RegisterManager_ = registerManager;	// mai: share with "formImportSummary".
			formImportSummary.ShowDialog(this);

			//==================================================================
			//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
			// mai: Bypass this ConfirmClosingDialog when finished all jobs:
			isByPassConfirmClosingDialog = true;
			this.Close();
			return;

			//@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        } // importFinished



		//##################################################################
		public DialogResult CustomFormErrorLongMsg__ShowDialog(params string[] args)
		{
			FormErrorLongMsg _form = new FormErrorLongMsg();
			DialogResult dialogResult = _form.CustomShowDialog_ret(
				args[0]
				,
				args[1]
			);
			return dialogResult;
		} // CustomFormErrorLongMsg__ShowDialog

		public DialogResult CustomFormErrorShortMsg__ShowDialog(params string[] args)
		{
			FormErrorShortMsg _form = new FormErrorShortMsg();
			DialogResult dialogResult = _form.CustomShowDialog_ret(
				args[0]
				,
				args[1]
			);
			return dialogResult;
		} // CustomFormErrorShortMsg__ShowDialog


		//==================================================================
		public DialogResult CustomFormError__DBF_TXT__Over12Files__ShowDialog(params string[] args)	// OK
		{
			FormErrorShortMsg _form = new FormErrorShortMsg();
			DialogResult dialogResult = _form.CustomShowDialog_ret(
				args[0]
				,
				args[1]
			);
			return dialogResult;
		} // CustomFormError__DBF_TXT__Over12Files__ShowDialog

		//=================================
		public DialogResult CustomFormError__TXT__InEqualityLenghtOfFieldNameAndFieldData__ShowDialog(params string[] args)	// OK
		{
			FormErrorLongMsg _form = new FormErrorLongMsg();
			DialogResult dialogResult = _form.CustomShowDialog_ret(
				args[0]
				,
				args[1]
			);
			return dialogResult;
		} // CustomFormError__TXT__InEqualityLenghtOfFieldNameAndFieldData__ShowDialog

		//=================================
		public DialogResult CustomFormError__DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__NotHasFieldName__ShowDialog(params string[] args)	// OK
		{
			FormErrorShortMsg _form = new FormErrorShortMsg();
			DialogResult dialogResult = _form.CustomShowDialog_ret(
				args[0]
				,
				args[1]
			);
			return dialogResult;
		} // CustomFormError__DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__NotHasFieldName__ShowDialog

		public DialogResult CustomFormError__DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__InValidDentistryData__ShowDialog(params string[] args)	// OK
		{
			FormError__ADDON_DESC__Dentistry _form = new FormError__ADDON_DESC__Dentistry();
			DialogResult dialogResult = _form.CustomShowDialog_ret(
				args[0]
				,
				args[1]
			);
			return dialogResult;
		} // CustomFormError__DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__InValidDentistryData__ShowDialog

 /*       //######################yaya#####################
        public DialogResult CustomFormError__DBF_TXT__OPD_CLAIM_CODE__NotHasFieldName__ShowDialog(params string[] args) // OK
        {
            FormErrorShortMsg _form = new FormErrorShortMsg();
            DialogResult dialogResult = _form.CustomShowDialog_ret(
                args[0]
                ,
                args[1]
            );
            return dialogResult;
        } // CustomFormError__DBF_TXT__CHAD__ADDON_DESC__ADDON_DETAIL__NotHasFieldName__ShowDialog
*/
        //##################################################################
        public void Custom__testShowDataTable__ShowDialog(DataTable dataTableOnTest)
		{
			FormTestDataGVw formTestDataGVw = new FormTestDataGVw();
			formTestDataGVw.dataGVw01_alias.DataSource = dataTableOnTest;
			formTestDataGVw.ShowDialog();
		} // Custom__testShowDataTable__ShowDialog

		//##################################################################

	} // FormImportEclaim_DBF_TXT

} // ns
