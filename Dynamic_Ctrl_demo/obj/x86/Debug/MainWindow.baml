   M S B A M L   `   `   `  ����  Dynamic_Ctrl_demo"    Dynamic_Ctrl_demo.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5      �  � RowDefinitions 5   
   �� 5      $
��288* �6   �� 5      $	��23* �6    � ColumnDefinitions 5
   
   �� 5      $
��253* �6   �� 5      $
��150* �6   �� 5      $
��100* �6   .+�5   
   �� -   5        $��	StPnlMain��5      $��5,5,5,17q�6&   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� -   5        $��tBoxDefault��5      $��50��6-   $��5q�68   �� 5      -   5        $��
btnDefault��5      $��23��6+   $��80��67   $��Default��6B   $��5q�6T    � ToolTip$$ Add text in textBox to ListBox��6x   5      5      �� 5   
   $��1��6   $��0,0,0,12q�6%   .+�5      �� -   5        $��btnAddNewCtrls��5      $��Add new Controls��6)   $��23��6D   $��5q�6P   $% Adds new controls in left panel��6x   �� 5      -   5        $��btnAddEventHandler��5      $��Add event handler��6/   $��23��6K   $��5q�6W   $$ Add eventhandler to new Button��6�   
��� � �6�   �� 5      -   5        $��btnRemoveEventHandler��5      $��Remove event handler��62   $��23��6Q   $��5q�6]   $' !Remove eventhandler to new Button��6�   
���  ��
�������6�   5      �� 5   
   -   5        $��listBox1��5      $��2��6"   $��5,5,5,17q�62   �� 5   
   -   5        $��
statusBar1��5   z   $��1��6   $��3��6!   $��23��65   $��Stretch=�6A   $��Bottom=�6_   .@�5      �� $F��@See tooltip on buttons. Try geen and red buttons more than once.��6   5      5      5      