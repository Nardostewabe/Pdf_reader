   M S B A M L   `   `   `  ����E  APDF_reader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PDF_reader.PDFReaderPDF_reader.PDFReader  5   	   "    PDF_reader.PDFReader.PdfView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PDF_reader.PDFReader     5   	     � Title$  PdfView��5   	   $	��450��6   $	��800��6&   .��5	      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   o� $��0��6   .@�5      m� $��_File��6   .@�5      m� -   5        $
��Exit��5      6)   5      m� 5      $��_View��6   .@�5      m� -   5        $��Zoom In��5      6,   m� 5      -   5        $��Zoom Out��5      6-   5      5      �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��5q�6;   .+�5      �� -   5        $��Previous��5      $	��100��6(   64   �� 5       -   5        $
��Next��5       $	��100��6$   60   5!      �� 5$   
   $��2��6   
���   �$��1q�62   $��10q�6F   ._�5%      �� -   5         ��Name$ PdfImage��5%      $��Uniform=�6&   5&      �� 5(   
   $��3��6   .@�5)      �� .��5*      �� -   5         ��Name$ 
PageStatus��5*      $��Page: 1��60   5+      �� 5,      .��5-      �� -   5        $��Zoom In��5-      6+   5.      �� 5/      .��50      �� -	   5        $��Zoom Out��50      6,   51      52      53      54      