   M S B A M L   `   `   `  ����D  @Instagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Instagram.ViewsInstagram.Views  5   	       Instagram.Views.StoryView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Instagram.Views     5   	     � Title$  Instagram Story��5   	   $	��520��6!    � 	Resources 5	       �� 
TargetType   buttonStyle �� Property �� Value��%�   &	        �� 5
   
   " ��6   .�5      ��  �@�Cursor$
 Hand��6'   �� 5       � ��
 ���� 6+   �� 5       � ��$ 0q�60   �� 5       � y�$ Visible=�6+   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $��0��6.   .+�5      �� ��6   �� *
Nickname+�� 5      �� 5      ��6   �� *PublicationDate+5      � 5   
   $��1��6   ���#000099 � Value 6#   �� *
StoryIndex+ � Maximum 6@   �� *HowManyStories+6c   �� 5   
   $
��Fill=�6/   $��2��6>   ��6   �� *ImageSource+6>   �� 5   
   $��2��6   $��30��6"   $ 
Horizontal=�6.   $	��Top=�6G   $��Right=�6_   ��5       �� 5!      " ��6   .�5"      ��  � y�$ Visible=�63   	 �� Triggers	 5#      �� 5$      
 �� Binding
 64   �� *IsUserAuthor+ �� Value	 False6&   .a�5%      ��  � y�$ 	Collapsed=�6;   5&      5(      .+�5*      �� $��30��6   $��30��6    #��[  6,   ��6Q   �� *AddStoryButton+��5+      �� 5,      ��6   �� *
PlusIcon+5.      �� 5/      $��30��6   $��30��6    #��[  6,   ��6Q   �� *DeleteStoryButton+��50      �� 51      ��6   �� *	TrashIcon+53      54      �� 55   
   $��3��6   $ 
Horizontal=�6"   .+�56      �� $	��260��69   $��50��6E   #��[  6Q   ��6   �� *LeftArrowButton+��57      �� 58      ��6   �� *LeftArrowIcon+5:      �� 5;      $	��260��6:   $��50��6F   #��[  6R   ��6   �� *RightArrowButton+��5<      �� 5=      ��6   �� *RightArrowIcon+5?      5@      5A      5B      