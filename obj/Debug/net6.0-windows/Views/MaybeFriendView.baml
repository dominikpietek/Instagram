   M S B A M L   `   `   `  ����D  @Instagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Instagram.ViewsInstagram.Views  5      ;"clr-namespace:Instagram.ConventersInstagram.Conventers  5      %    Instagram.Views.MaybeFriendView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:Instagram.Views     5      0c"clr-namespace:Instagram.Conventers     5        � 	Resources  5	      *   $Instagram.Conventers.CenterConverter   CenterConverter-   'Instagram.Conventers.HalfValueConverter  HalfValueConverter��%:   &	        &	        5
   
   6     5   
   6   5      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��180 �6   �� 5      $��80 �6   .+�5   
   �� $��0��68    � Cursor$
 Hand��6H   
������ 
������ ��6   �� *CheckProfileButton+.��5      �  5      �� 5      $��80 �6'   �� 5      $	��100 �6'   .+�5      A� -   5         A�Name$ ElipsePhoto��5   1   $��70��6   $��70��6%   $��0��6F   ��5      �� 5       �� ImageSource 6%   �� *ProfilePhoto+ J� Clip 5      @� 5       @� RadiusX 5      ��  �� Path$ Width� �� 	Converter# [ 	 �� ElementName$	 ElipsePhoto��
 @� RadiusY
 5      �� $ Height�# [ $	 ElipsePhoto�� @� Center 5       �� $ Width�# [  $	 ElipsePhoto��5!      5#      �� 5$      $��1��65   ��6   �� *
Nickname+65   5%      5&      �� 5'   
   $��1��63   $
 Hand��6C   
������ ��6   �� *AddUserButton+��5(      �� 5)       �� 
TargetType" ��6   .�5*      ��  �� Property � �� �� Value
 ���� 63    �� Triggers 5+      �� 5,       �� Binding 6&   �� *IsInvitationSent+ �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=� �� Value True6}   .a�5-      ��  � ��
 �  ��6;   5.      50      ��52      �� 53      ��6   �� *AddUserIconPath+55      56      57      