   M S B A M L   `   `   `  ���� D  @Instagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Instagram.ConventersInstagram.Conventers  1clr-namespace:Instagram.ViewsInstagram.Views  +    %Instagram.Views.FriendInMessengerView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0c"clr-namespace:Instagram.Conventers     /localclr-namespace:Instagram.Views     $	��100��  � 	Resources  *   $Instagram.Conventers.CenterConverter   CenterConverter-   'Instagram.Conventers.HalfValueConverter  HalfValueConverter��%   &	        &	          .���  � ColumnDefinitions �� $��* ��� $	��100 �.+��� $��0�� � Cursor$
 Hand��
������ $
��Left=�$	��420��$��0q����� *OpenChatButton+����  �� Orientation$ 
Horizontal=�$	��420��.+�A� -    A�Name$ ElipsePhoto��$	��100��$	��100�� �� Stretch$ Uniform=�����  �� ImageSource �� *ProfilePhotoSource+ J� Clip @�  @� RadiusX �� 	 �� Path$	 Width�
 �� 	Converter#
 [  �� ElementName$ ElipsePhoto�� @� RadiusY �� $	 Height�#
 [ $ ElipsePhoto�� @� Center �� $	 Width�#
 [  $ ElipsePhoto���� $ Vertical=�.+��� $��16(�$��6q����� *
Nickname+�� $��16(�$��6q����� *LastMessage+�� $��1��$
 Hand��
������ $��0q����� *RemoveFriendButton+.���� ���� *TrashIconPath+