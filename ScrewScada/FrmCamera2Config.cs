using HalconDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ScrewScada
{
    public partial class FrmCamera2Config : DockContent
    {

        #region 字段属性
        // Local iconic variables 

        HObject ho_Image, ho_ModelContour, ho_MeasureContour;
        HObject ho_Contours, ho_ROI_0, ho_Image1, ho_ImageModel;
        HObject ho_RegionDilation, ho_ImageReduced, ho_ModelContours;
        HObject ho_ContoursAffineTrans, ho_ImageEmphasize = null;
        HObject ho_Contour1 = null, ho_UsedEdges = null, ho_Cross2 = null;
        HObject ho_ResultContours = null;

        // Local control variables 

        HTuple hv_WindowHandle = new HTuple(), hv_MetrologyHandle = new HTuple();
        HTuple hv_Row = new HTuple(), hv_Column = new HTuple();
        HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
        HTuple hv_Width = new HTuple(), hv_Height = new HTuple();
        HTuple hv_ModelID = new HTuple(), hv_Area = new HTuple();
        HTuple hv_HomMat2D = new HTuple(), hv_RectangleParam = new HTuple();
        HTuple hv_RectIndices = new HTuple(), hv_ImageFiles = new HTuple();
        HTuple hv_Index = new HTuple(), hv_Row3 = new HTuple();
        HTuple hv_Column3 = new HTuple(), hv_Angle3 = new HTuple();
        HTuple hv_Scale = new HTuple(), hv_Score = new HTuple();
        HTuple hv_Length = new HTuple(), hv_UsedRow = new HTuple();
        HTuple hv_UsedColumn = new HTuple(), hv_Length1R = new HTuple();
        HTuple hv_Length2R = new HTuple(), hv_Parameter = new HTuple();
        HTuple hv_Parameter1 = new HTuple(), hv_Parameter2 = new HTuple();

        Module.Camera2Serializable c2s;

        Module.SequenceModules.SeqGrayImage seqGray = new Module.SequenceModules.SeqGrayImage();
        
        #endregion

        public FrmCamera2Config()
        {
            InitializeComponent();
            this.Load += FrmCamera2Config_Load;
        }
        private void FrmCamera2Config_Load(object sender, EventArgs e)
        {
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ModelContour);
            HOperatorSet.GenEmptyObj(out ho_MeasureContour);
            HOperatorSet.GenEmptyObj(out ho_Contours);
            HOperatorSet.GenEmptyObj(out ho_ROI_0);
            HOperatorSet.GenEmptyObj(out ho_Image1);
            HOperatorSet.GenEmptyObj(out ho_ImageModel);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_Contour1);
            HOperatorSet.GenEmptyObj(out ho_UsedEdges);
            HOperatorSet.GenEmptyObj(out ho_Cross2);
            HOperatorSet.GenEmptyObj(out ho_ResultContours);
            seqGray.OnImageFileLoad += SeqGray_OnImageFileLoad;
            propertyGrid1.SelectedObject = seqGray;
            c2s = Module.SerializableTool.FromByFile<Module.Camera2Serializable>("cam2.sol");
        }

        private void SeqGray_OnImageFileLoad(object sender, string path)
        {
            HOperatorSet.ReadImage(out ho_Image,path);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.SetPart(hWindow_Final1.hWindowControl.HalconWindow, 0, 0, hv_Height, hv_Width);
            //HOperatorSet.DispObj(ho_Image, hWindow_Final1.hWindowControl.HalconWindow);
            hWindow_Final1.viewWindow.displayImage(ho_Image);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Camera2();
            seqGray.Length2 += 1.0;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Camera3();
        }
        //run
        private void button3_Click(object sender, EventArgs e)
        {
            if (seqGray.ImageSourse != Module.SequenceModules.SeqGrayImage.eImageSourse.File && string.IsNullOrEmpty(seqGray.ImageFile)) return;
           
            Run();
            propertyGrid1.SelectedObject = null;
            propertyGrid1.SelectedObject = seqGray;
        }


        private void FrmCamera2Config_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result =  MessageBox.Show("是否保存更改？","提示",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
            if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else if (result == DialogResult.Yes)
            {
                //do something...
            }

        }
        private void Run()
        {
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                // ho_Image.Dispose();
                //HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.SetPart(hWindow_Final1.hWindowControl.HalconWindow, 0, 0, hv_Height, hv_Width);
            }
            ho_ImageEmphasize.Dispose();

            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, hv_Width, hv_Height,
                1);
            //HOperatorSet.DispObj(ho_Image, hWindow_Final1.hWindowControl.HalconWindow);
           // hWindow_Final1.hWindowControl.HalconWindow.DispImage((HImage)ho_Image);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.FindScaledShapeModel(ho_Image, c2s.Modle, (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), 0.5, 5, 0.6, 0, 0.5, "none", 0, 0.3,
                    out hv_Row3, out hv_Column3, out hv_Angle3, out hv_Scale, out hv_Score);
            }
            HOperatorSet.TupleLength(hv_Score, out hv_Length);
            if ((int)(new HTuple(hv_Length.TupleEqual(0))) != 0)
            {
                //disp_message(hWindow_Final1.hWindowControl.HalconWindow, "failed!!!!!!!!!!!!!!!", "window", 20,
                //    20, "black", "true");
                //disp_continue_message(hWindow_Final1.hWindowControl.HalconWindow, "black", "true");
                ////wait_seconds (2)
                //HDevelopStop();
            }
            else
            {
                //hom_mat2d_identity (HomMat2DIdentity)
                //hom_mat2d_translate (HomMat2DIdentity, Row, Column, HomMat2DTranslate)
                //hom_mat2d_rotate (HomMat2DTranslate, Angle, Row, Column, HomMat2DRotate)
                //hom_mat2d_scale (HomMat2DRotate, Scale, Scale, Row, Column, HomMat2DScale)
                //affine_trans_contour_xld (ModelContours, ModelTrans, HomMat2DScale)
                //dev_display (ModelTrans)
                //下面开始测量尺寸

                HOperatorSet.AlignMetrologyModel(c2s.MetrologyHandle, hv_Row3, hv_Column3,
                    hv_Angle3);

                ho_ModelContour.Dispose();
                HOperatorSet.GetMetrologyObjectModelContour(out ho_ModelContour, c2s.MetrologyHandle,
                    "all", 1.5);
                //向图像应用测量模型
                HOperatorSet.ApplyMetrologyModel(ho_Image, c2s.MetrologyHandle);
                //获取测量卡尺
                ho_Contour1.Dispose();
                HOperatorSet.GetMetrologyObjectMeasures(out ho_Contour1, c2s.MetrologyHandle,
                    "all", "all", out hv_Row, out hv_Column);
                HOperatorSet.TupleLength(hv_Score, out hv_Length);
                if ((int)(new HTuple(hv_Length.TupleEqual(0))) != 0)
                {

                }
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, "all", "all", "used_edges",
                    "row", out hv_UsedRow);
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, "all", "all", "used_edges",
                    "column", out hv_UsedColumn);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_UsedEdges.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_UsedEdges, hv_UsedRow, hv_UsedColumn,
                        10, (new HTuple(45)).TupleRad());
                }
                //提取矩形边的长度
                hv_RectIndices = 0;
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                    "all", "result_type", "length1", out hv_Length1R);
                seqGray.Length1 = hv_Length1R.Length >0? hv_Length1R.D : 0D;
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                    "all", "result_type", "length2", out hv_Length2R);
                seqGray.Length2 = hv_Length2R.Length > 0 ? hv_Length2R.D : 0D;
                //中心坐标及角度
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                    "all", "result_type", "row", out hv_Parameter);
                seqGray.CenterX = hv_Parameter.Length > 0 ? hv_Parameter.D : 0D;;
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                    "all", "result_type", "column", out hv_Parameter1);
                seqGray.CenterY = hv_Parameter1 > 0 ? hv_Parameter1.D : 0D;
                HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                    "all", "result_type", "phi", out hv_Parameter2);
                seqGray.Rotate = hv_Parameter2 > 0 ? hv_Parameter2.D : 0D;
                HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "red");
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Cross2.Dispose();
                    HOperatorSet.GenCrossContourXld(out ho_Cross2, hv_Parameter, hv_Parameter1,
                        20, (new HTuple(45)).TupleRad());
                }
                //提取直线
                //get_metrology_object_result (MetrologyHandle, 0, 'all', 'result_type', 'all_param', ParamLine1)
                //get_metrology_object_result_contour (Contour, MetrologyHandle, 0, 'all', 1.5)
                ho_ResultContours.Dispose();
                HOperatorSet.GetMetrologyObjectResultContour(out ho_ResultContours, c2s.MetrologyHandle,
                    "all", "all", 1.5);
                HOperatorSet.DispObj(ho_ImageEmphasize, hWindow_Final1.hWindowControl.HalconWindow);
                HOperatorSet.SetLineWidth(hWindow_Final1.hWindowControl.HalconWindow, 1);
                HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "light gray");
                HOperatorSet.DispObj(ho_Contour1, hWindow_Final1.hWindowControl.HalconWindow);
                HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "green");
                HOperatorSet.SetLineWidth(hWindow_Final1.hWindowControl.HalconWindow, 2);
                HOperatorSet.DispObj(ho_ResultContours, hWindow_Final1.hWindowControl.HalconWindow);
                HOperatorSet.SetLineWidth(hWindow_Final1.hWindowControl.HalconWindow, 1);
                HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "white");
                HOperatorSet.DispObj(ho_UsedEdges, hWindow_Final1.hWindowControl.HalconWindow);
            }
        }
        private void Camera2()
        {
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, "G:/防火砖缺陷检测/Image_20201109161631787.bmp");
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.SetPart(hWindow_Final1.hWindowControl.HalconWindow,0,0, hv_Height, hv_Width);
            HOperatorSet.DispObj(ho_Image, hWindow_Final1.hWindowControl.HalconWindow);
            //gen_rectangle2 (ROI_0, 1368.71, 2059.78, rad(-167.642), 1471.49, 851.652)
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                ho_ROI_0.Dispose();
                HOperatorSet.GenRectangle2(out ho_ROI_0, 1049.75, 1241.71, 0.108671, 1082.21, 632.588);
            }
            ho_Contours.Dispose();
            HOperatorSet.GenContourRegionXld(ho_ROI_0, out ho_Contours, "border");
            ho_Image1.Dispose();
            HOperatorSet.GenImageConst(out ho_Image1, "byte", hv_Width, hv_Height);
            ho_ImageModel.Dispose();
            HOperatorSet.PaintXld(ho_Contours, ho_Image1, out ho_ImageModel, 128);
            ho_RegionDilation.Dispose();
            HOperatorSet.DilationCircle(ho_ROI_0, out ho_RegionDilation, 3.5);
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageModel, ho_RegionDilation, out ho_ImageReduced
                );
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.CreateScaledShapeModel(ho_ImageReduced, "auto", (new HTuple(0)).TupleRad()
                    , (new HTuple(360)).TupleRad(), "auto", 0.9, 1.1, "auto", "auto", "use_polarity",
                    "auto", "auto", out hv_ModelID);
                c2s.Modle = hv_ModelID;
            }
            ho_ModelContours.Dispose();
            HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_ModelID, 1);
            HOperatorSet.DispObj(ho_ModelContours, hWindow_Final1.hWindowControl.HalconWindow);
            HOperatorSet.AreaCenter(ho_ROI_0, out hv_Area, out hv_Row1, out hv_Column1);
            HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_Row1, hv_Column1, 0, out hv_HomMat2D);
            ho_ContoursAffineTrans.Dispose();
            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffineTrans,
                hv_HomMat2D);
            HOperatorSet.CreateMetrologyModel(out hv_MetrologyHandle);
            HOperatorSet.SetMetrologyModelImageSize(hv_MetrologyHandle, hv_Width, hv_Height);

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RectangleParam = new HTuple();
                hv_RectangleParam[0] = 1049.75;
                hv_RectangleParam[1] = 1241.71;
                hv_RectangleParam = hv_RectangleParam.TupleConcat(0.108671 );
                hv_RectangleParam = hv_RectangleParam.TupleConcat(new HTuple(1082.21, 632.588));
            }
            HOperatorSet.AddMetrologyObjectGeneric(hv_MetrologyHandle, "rectangle2", hv_RectangleParam,
                80, 20, 6, 15, new HTuple(), new HTuple(), out hv_RectIndices);
            //Line1 := [879,784,558,3426]
            //add_metrology_object_generic (MetrologyHandle, 'line', [Line1], 20, 5, 1, 30, [], [], LineIndices)
            HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_length1",
                80);

            HOperatorSet.SetMetrologyObjectParam(hv_MetrologyHandle, "all", "measure_transition",
                "all");
            //检查已添加到计量模型中的形状
            //获取测量轮廓
            ho_ModelContour.Dispose();
            HOperatorSet.GetMetrologyObjectModelContour(out ho_ModelContour, hv_MetrologyHandle,
                "all", 1.5);
            //获取测量卡尺
            ho_MeasureContour.Dispose();
            HOperatorSet.GetMetrologyObjectMeasures(out ho_MeasureContour, hv_MetrologyHandle,
                "all", "all", out hv_Row, out hv_Column);
            //find_shape_model对齐计量模型

            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.SetMetrologyModelParam(hv_MetrologyHandle, "reference_system", ((hv_Row1.TupleConcat(
                    hv_Column1))).TupleConcat(0));
            }
            c2s.MetrologyHandle = hv_MetrologyHandle;
            Module.SerializableTool.Save2File<Module.Camera2Serializable>(c2s, "cam2.sol");
        
            
        }
        private void Camera3()
        {
            
            c2s = Module.SerializableTool.FromByFile<Module.Camera2Serializable>("cam2.sol");
            //Image Acquisition 01: Code generated by Image Acquisition 01
            hv_ImageFiles = new HTuple();
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[0] = "G:/防火砖缺陷检测/Image_20201109161631787.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[1] = "G:/防火砖缺陷检测/Image_20201109161638427.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[2] = "G:/防火砖缺陷检测/Image_20201109161641675.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[3] = "G:/防火砖缺陷检测/Image_20201109161651523.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[4] = "G:/防火砖缺陷检测/Image_20201109161654667.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[5] = "G:/防火砖缺陷检测/Image_20201109161701411.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[6] = "G:/防火砖缺陷检测/Image_20201109161704812.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[7] = "G:/防火砖缺陷检测/Image_20201109161707571.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[8] = "G:/防火砖缺陷检测/Image_20201109161715763.bmp";
            if (hv_ImageFiles == null)
                hv_ImageFiles = new HTuple();
            hv_ImageFiles[9] = "G:/防火砖缺陷检测/Image_20201109161720699.bmp";

            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                    HOperatorSet.SetPart(hWindow_Final1.hWindowControl.HalconWindow, 0, 0, hv_Height, hv_Width);
                }
                ho_ImageEmphasize.Dispose();
               
                HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, hv_Width, hv_Height,
                    1);
                HOperatorSet.DispObj(ho_Image, hWindow_Final1.hWindowControl.HalconWindow);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.FindScaledShapeModel(ho_Image, c2s.Modle, (new HTuple(0)).TupleRad()
                        , (new HTuple(360)).TupleRad(), 0.7, 1.3, 0.6, 0, 0.5, "none", 0, 0.3,
                        out hv_Row3, out hv_Column3, out hv_Angle3, out hv_Scale, out hv_Score);
                }
                HOperatorSet.TupleLength(hv_Score, out hv_Length);
                if ((int)(new HTuple(hv_Length.TupleEqual(0))) != 0)
                {
                    //disp_message(hWindow_Final1.hWindowControl.HalconWindow, "failed!!!!!!!!!!!!!!!", "window", 20,
                    //    20, "black", "true");
                    //disp_continue_message(hWindow_Final1.hWindowControl.HalconWindow, "black", "true");
                    ////wait_seconds (2)
                    //HDevelopStop();
                }
                else
                {
                    //hom_mat2d_identity (HomMat2DIdentity)
                    //hom_mat2d_translate (HomMat2DIdentity, Row, Column, HomMat2DTranslate)
                    //hom_mat2d_rotate (HomMat2DTranslate, Angle, Row, Column, HomMat2DRotate)
                    //hom_mat2d_scale (HomMat2DRotate, Scale, Scale, Row, Column, HomMat2DScale)
                    //affine_trans_contour_xld (ModelContours, ModelTrans, HomMat2DScale)
                    //dev_display (ModelTrans)
                    //下面开始测量尺寸

                    HOperatorSet.AlignMetrologyModel(c2s.MetrologyHandle, hv_Row3, hv_Column3,
                        hv_Angle3);

                    ho_ModelContour.Dispose();
                    HOperatorSet.GetMetrologyObjectModelContour(out ho_ModelContour, c2s.MetrologyHandle,
                        "all", 1.5);
                    //向图像应用测量模型
                    HOperatorSet.ApplyMetrologyModel(ho_Image, c2s.MetrologyHandle);
                    //获取测量卡尺
                    ho_Contour1.Dispose();
                    HOperatorSet.GetMetrologyObjectMeasures(out ho_Contour1, c2s.MetrologyHandle,
                        "all", "all", out hv_Row, out hv_Column);
                    HOperatorSet.TupleLength(hv_Score, out hv_Length);
                    if ((int)(new HTuple(hv_Length.TupleEqual(0))) != 0)
                    {

                    }
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, "all", "all", "used_edges",
                        "row", out hv_UsedRow);
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, "all", "all", "used_edges",
                        "column", out hv_UsedColumn);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_UsedEdges.Dispose();
                        HOperatorSet.GenCrossContourXld(out ho_UsedEdges, hv_UsedRow, hv_UsedColumn,
                            10, (new HTuple(45)).TupleRad());
                    }
                    //提取矩形边的长度
                    hv_RectIndices = 0;
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                        "all", "result_type", "length1", out hv_Length1R);
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                        "all", "result_type", "length2", out hv_Length2R);
                    //中心坐标及角度
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                        "all", "result_type", "row", out hv_Parameter);
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                        "all", "result_type", "column", out hv_Parameter1);
                    HOperatorSet.GetMetrologyObjectResult(c2s.MetrologyHandle, hv_RectIndices,
                        "all", "result_type", "phi", out hv_Parameter2);
                    HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "red");
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Cross2.Dispose();
                        HOperatorSet.GenCrossContourXld(out ho_Cross2, hv_Parameter, hv_Parameter1,
                            20, (new HTuple(45)).TupleRad());
                    }
                    //提取直线
                    //get_metrology_object_result (MetrologyHandle, 0, 'all', 'result_type', 'all_param', ParamLine1)
                    //get_metrology_object_result_contour (Contour, MetrologyHandle, 0, 'all', 1.5)
                    ho_ResultContours.Dispose();
                    HOperatorSet.GetMetrologyObjectResultContour(out ho_ResultContours, c2s.MetrologyHandle,
                        "all", "all", 1.5);
                    HOperatorSet.DispObj(ho_ImageEmphasize, hWindow_Final1.hWindowControl.HalconWindow);
                    HOperatorSet.SetLineWidth(hWindow_Final1.hWindowControl.HalconWindow, 1);
                    HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "light gray");
                    HOperatorSet.DispObj(ho_Contour1, hWindow_Final1.hWindowControl.HalconWindow);
                    HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "green");
                    HOperatorSet.SetLineWidth(hWindow_Final1.hWindowControl.HalconWindow, 2);
                    HOperatorSet.DispObj(ho_ResultContours, hWindow_Final1.hWindowControl.HalconWindow);
                    HOperatorSet.SetLineWidth(hWindow_Final1.hWindowControl.HalconWindow, 1);
                    HOperatorSet.SetColor(hWindow_Final1.hWindowControl.HalconWindow, "white");
                    HOperatorSet.DispObj(ho_UsedEdges, hWindow_Final1.hWindowControl.HalconWindow);
                    ho_Image.Dispose();
                }
            }
            hv_ImageFiles.Dispose();
            GC.Collect();
        }
    }
}
