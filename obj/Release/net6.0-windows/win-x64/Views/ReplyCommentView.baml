   M S B A M L   `   `   `  ���� D  @Instagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Instagram.ConventersInstagram.Conventers  &     Instagram.Views.ReplyCommentView   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0c"clr-namespace:Instagram.Conventers       � 	Resources  *   $Instagram.Conventers.CenterConverter   CenterConverter-   'Instagram.Conventers.HalfValueConverter  HalfValueConverter  buttonStyle �� 
TargetType �� Property �� Value  	redButton �� BasedOn �� Triggers �� Binding �� UpdateSourceTrigger �� Mode	 �� Value��%)  &	        &	      &	 
     &	 c     7      �� " ��.���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$ 0q��� " ��8   .���  � ��
 ����  ��  �� *IsCommentLiked+$ PropertyChanged=�$ TwoWay=�	 True.a���  � �� �#FF7F7F.��� 
 � ColumnDefinitions
 �� $��50 ��� $��40 ��� $��* � � RowDefinitions �� $��30 ��� $��30 ��� $��* ��� $��30 �.+��� $
��23 0q�$��0��$��0��$��4��$��4q�
���   �A� -    A�Name$ ElipsePhoto��$��40��$��40��$��1��$��0��$��2������  �� ImageSource �� *CommentProfilePhotoSource+ J� Clip @�  @� RadiusX ��  �� Path$ Width� �� 	Converter# [  �� ElementName$ ElipsePhoto�� @� RadiusY �� $ Height�# [ $ ElipsePhoto�� @� Center �� $ Width�# [  $ ElipsePhoto��]� $��0��$��2��.+��� $
��Left=����� *CommentProfileName+�� $��60��$��5q����� *RemoveButton+���� ���� *TrashIconPath+���� " ��.���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$ 0q� ��  �� *IsReplyYours+$ PropertyChanged=�$ TwoWay=�		 false.a���  � y�$ 	Collapsed=��� $��1��$��2��$��2�� �� Padding$ 5q����� *CommentText+��  �� Orientation$ 
Horizontal=�$��3��$��1��$��2��.+��� $��0 0 20 0q����� *PublicationDate+�� $��0 0 20 0q����� *LikesNumber+$ PropertyChanged=��� $��0 0 20 0q�$��6q�#���   � Cursor$
 Hand������ *
LikeButton+���� ���� *LikeIconPath+