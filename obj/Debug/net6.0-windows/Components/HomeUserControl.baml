   M S B A M L   `   `   `  ����D  @Instagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Instagram.ComponentsInstagram.Components  5      *    $Instagram.Components.HomeUserControl   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Instagram.Components     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $��* �6   �� 5      $��30 �6   .+�5   
   �� $��There are no posts :(��6   $��22(�60   $��Black"�6>   �� 5   
   
���������6   �� *
HomeSource+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5      �� 5   
   $��1��6   $��No more Posts!��6   $��Center=�66   �� 5   
   $��1��6    � Cursor$
 Hand��6A   ��6   �� *LoadMoreButton+$��	LOAD MORE��5      ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � y� �� Value$ Visible=�63    �� Triggers 5      �� 5       �� Binding 6&   �� *IsThereMorePosts+$ PropertyChanged=�$ TwoWay=�	 �� Value		 False6}   .a�5      ��  � y�$ 	Collapsed=�6;   5      5      5      5       5!      