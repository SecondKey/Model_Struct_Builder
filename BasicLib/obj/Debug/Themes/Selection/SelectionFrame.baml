   M S B A M L   `   `   `  ����C  ?BasicLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:BasicLibBasicLib  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source    BasicLib.SelectionFrame �� 
TargetType �� Property �� Value �Name   BasicLib.DragThumb � Cursor   Kind   ThumbHLineTemplate  ThumbVLineTemplate  ThumbRectTemplate ��Name  SelectionTemplate	 �� Triggers
 ^� 
Conditions �� Binding �� Value �� 
TargetName�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5       localclr-namespace:BasicLib  5        5      �� 5   
   $, &/BasicLib;component/Themes/Shared.xaml?�6   %?  '         7   7  7  7   7  7  7  7  7  �� 5
      "   6   .�5   
   ��  � �� 5      �� 5      .R�5      � .+�5      � $ PART_ResizeFrame��64   $y�Visible=�6   .+�5        $��3��6.   $ SizeNS��69   $��0 -1 0 0q�6I   $	 Top=�6\   $	��Top=�5      $��Stretch=�6+   8��  6I     5      $��3��6.   $ SizeWE��68   $��-1 0 0 0q�6H   $
 Left=�6[   $��Stretch=�5   
   $
��Left=�6&   8�� 6A     5      $��3��6.   $ SizeWE��68   $��0 0 -1 0q�6H   $ Right=�6Z   $��Stretch=�5      $��Right=�6$   8�� 6@     5      $��3��6.   $ SizeNS��69   $��0 0 0 -1q�6I   $ Bottom=�6[   $��Bottom=�5      $��Stretch=�6#   8�� 6A     5      $��7��6.   $��7��68   $ SizeNWSE��6C   $��	-3 -3 0 0q�6U   $ TopLeft=�6h   $	��Top=�5      $
��Left=�6    8�� 6;     5      $��7��6.   $��7��68   $ SizeNESW��6C   $��	0 -3 -3 0q�6U   $ TopRight=�6i   $	��Top=�5      $��Right=�6    8�� 6<     5      $��7��6.   $��7��68   $ SizeNESW��6C   $��	-3 0 0 -3q�6U   $ 
BottomLeft=�6i   $��Bottom=�5      $
��Left=�6#   8�� 6>     5      $��7��6.   $��7��68   $ SizeNWSE��6C   $��	0 0 -3 -3q�6U   $ BottomRight=�6i   $��Bottom=�5      $��Right=�6#   8�� 6?   5       �� 5!      $ PART_SelectionFrame��67   $y�Visible=�6"   8�� 5"      5#      	 5%      ^� 5&      
 5'      �� 5(   "    69   �� *%#AllFeature[ItemSelected].IsSelected+ True6,   69   .0�5+      �� $ PART_ResizeFrame��6%    � y�$ Visible=�6Y   5,      ^� 5-      
 5.      �� 5/   "    69   �� *%#AllFeature[ItemSelected].IsSelected+ True6,   69   .0�52      �� $ PART_SelectionFrame��6%    � y�$ Visible=�6\   53      55      57      58      59      