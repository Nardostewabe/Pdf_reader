   M S B A M L   `   `   `  ����E  APDF_reader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:PDF_reader.Pdf_ManagerPDF_reader.Pdf_Manager  5   	   "    PDF_reader.Pdf_Manager.Mypdf   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:PDF_reader.Pdf_Manager     5   	     � Title$  Mypdf��5   	   $	��450��6   $	��800��6$   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $
��283* �6   �� 5      $
��517* �6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   $��5,5,5,5q�6;   $��2��6L   .+�5      �� -   5        $��Add PDF��5      $	��100��6'   63   �� 5      -   5        $��Download PDF��5      $	��150��6,   68   �� 5      -   5        $��My Downloads��5      $	��150��6,   68   5      �� 5   
   -   5         ��Name$ PdfListView��5       �� DisplayMemberPath$ FileName��6(    �� SelectionMode$ Single=�6E   $��2��6\   $��1��6p    �� View 5      �� 5      .P�5      �� $��PDF Name��6%    �� Width$	 300��67   	 �� DisplayMemberBinding	 6C   �� *
FileName+�� 5       $��
Date Added��6%   $	 200��69   	 6E   �� *	DateAdded+5!      5#      �� 5%   
   $��2��6   $��2��6!   .@�5&      �� .��5'      �� -   5        
 ��Name$
 
StatusText��5'      $��Total PDFs: 0��60   5(      5)      5*      5+      