   M S B A M L   `   `   `  ����  WpfAsyncDelegate    WpfAsyncDelegate.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      -   5          � Title$  Window1��5      $	��329��6   $	��331��6"   .��5      �  � RowDefinitions 5   
   �� 5      $
��220* �6   �� 5      $	��50* �6   .+�5
   
   �� -   5        $��	lbxPrimes��5
   A   $
��Left=�6   $��
11,12,0,33q�6-   $	��120��6R   �� 5   
   -   5        $��btnStartWorker��5   L   $��23��6   $��Right=�6   $��	0,12,40,0q�69   $	��Top=�6b   $	��107��6z   .��5   .   Start worker thread6C   �� 5   
   -   5        $��btnCancelWorker��5   L   $��23��6   $��Right=�6   $��	0,50,40,0q�69   $	��Top=�6c   $	��106��6{   .��5   /   Stop worker thread6C   �� 5   
   -   5        $��	lblStatus��5   K   $��28��6   $��Right=�6   $��	0,0,27,33q�68   $��Bottom=�6\   $	��120��6w   .��6�   Worker not started6�   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��2��6;   .+�5      � -   5        $��	pgbPrimes��5   =   $��12��6   $	��200��6&   $��5q�62   6=   �� 5      -   5        $��lblProgress��5   G   $��28��6   $��Right=�6    $��5q�6<   .��6Z   Label6a   5      5      5      