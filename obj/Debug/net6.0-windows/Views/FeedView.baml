   M S B A M L   `   `   `  ����D  @Instagram, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Instagram.ConventersInstagram.Conventers  5   	   9!clr-namespace:Instagram.BehaviorsInstagram.Behaviors  5   	       Instagram.Views.FeedView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0c"clr-namespace:Instagram.Conventers     5   	   /b!clr-namespace:Instagram.Behaviors     5   	     � Title$  	Instagram��5	   	   $	��800��5
   	   $	��840��6   $	��800��5   	   $	��840��6    � 	Resources 5      *   $Instagram.Conventers.CenterConverter   CenterConverter-   'Instagram.Conventers.HalfValueConverter  HalfValueConverter  
dictionary �� MergedDictionaries �� Source��%�   &	        &	      &	 &       5   
   6     5   
   6   �� 5   
    5      �� 5      $= 7../ResourceDictionaries/ButtonValidationDictionary.xaml?�6%   5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��320 �6   �� 5      $	��200 �6   �� 5      $	��260 �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5       $	��100 �6   �� 5!      $��30 �6   �� 5"      $	��500 �6   �� 5#      $��50 �6   �� 5$      $��* �6   .+�5&   
   �� $
��Left=�60   $��1��6F   $��40��6V   $��0��6b   $
��Left=�6o   $��Center=�6�   ��6   �� *
HomeButton+.��5'      �� ��6   �� *
LogoPath+��5(      �� 5)       �� 
TargetType" ��6   .�5*      ��  �� Property � �� �� Value
 ���� 64   �� 5+       � ��
 ���� 63   �� 5,       �@�Cursor$
 Hand��6/   5-      5/      �� 50   
   $��Center=�62   $��2��6O   $��40��6_   $	��200��6k   $��0��6w   	 � Cursor$
	 Hand��6�   ��6   �� *SearchButton+��51      �� 52      
 �� Orientation$
 
Horizontal=�6   .+�53      �� $��Center=�6>   $��20��6Y   ��6   �� *SearchIconPath+6Y   �� 54      $��SEARCH��6   $
��Bold"�6-   $��Center=�6?   
���   �6Z   55      ��57      �� 58      " ��6   .�59      ��  � y�$ Visible=�63    �� Triggers 5:      �� 5;       �� Binding 6&   �� *IsSearchClicked+ �� Value true6J   .a�5<      ��  � y�$ 	Collapsed=�6;   5=      5?      5A      �� 5B   
   -   5         ��Name$ searchingBox��5C       $��2��5B      $��0��6"   $��50��6/   $	��200��6;   $��Center=�6G   $��10q�6b   $��16(�5C      2   ,Instagram.Behaviors.TextBoxIsFocusedBehavior   IsFocusedCommand 5D      �� *	LostFocus+4   .Instagram.Behaviors.TextBoxTextChangedBehavior   TextChangedCommand 5E      �� *ChangeSearching+��5F      �� *SearchingText+ �� UpdateSourceTrigger$ PropertyChanged=�.��6V   ��5G      �� 5H      " ��6   .�5I      ��  � y�$ Visible=�63    5J      �� 5K       6&   �� *IsSearchClicked+	 false6J   .a�5L      ��  � y�$ 	Collapsed=�6;   5M      5O      .��5P      5Q      o� 5R   
   $��Right=�6   $��3��6&   $��40��66   $��0��6B   $��Center=�6O   $��Right=�6j   .@�5S      m� $
	 Hand��66   $��0 0 10 0q�6D    m� Command 6   �� *
HomeButton+��5T      �� 5U      ��6   �� *HomeIconPath+5W      m� 5X      $
	 Hand��6;    6   �� *MessengerButton+��5Y      �� 5Z      $
��10 0q�6?   ��6   �� *MessageIconPath+6?   5\      m� 5]      $
	 Hand��6   $��10 0 0 0q�6%   ��5^      A� 5_      -   5         A�Name$ ElipsePhoto��5_   5   $��38��6   $��38��6)   ��5`      �� 5a       �� ImageSource 6)   �� *ProfilePhotoSource+ J� Clip 5c      @� 5d       @� RadiusX 5e      ��  �� Path$ Width� �� 	Converter# [  �� ElementName$ ElipsePhoto�� @� RadiusY 5f      �� $ Height�# [ $ ElipsePhoto�� @� Center 5g      �� $ Width�# [  $ ElipsePhoto��5h      5j      .@�5l      m�  6   �� *LogoutButton+ m� Icon 5m      �� 5n      ��6    �� *
LogoutPath+$��logout��5r   '   5s      m� 5t       6   �� *ProfileButton+ 5u      �� 5v      ��6    �� *ProfilePhotoSource+$��manage your profile��5z   '   5{      m� 5|       m� IsCheckable$
 True��6    6.   �� *DarkModeButton+ m� 	IsChecked 6Q   �� *IsDarkModeOn+$ PropertyChanged=�$��	dark mode��5   '   5�      m� 5�      $
 True��6    6.   �� *SendEmailsButton+$��send emails��5�   '   5�      5�      5�      �� 5�   
   $��2��6   $��1��6$   $��Visible=�64   $��Disabled=�6\   $��2��6�   .��5�      �� ��6   �� *StoriesSection+��5�      �� 5�      .R�5�      �� $
 
Horizontal=�6%   5�      5�      5�      �� 5�   
   -   5         ��Name$ searchScroll��5�   y   $��1��6   $��2��6$   $��3��64   $	��200��6E   $	��Top=�6Q   $	��140��6i   ��5�      �� 5�      " ��6   .�5�      ��  � y�$ Visible=�63   �� 5�       � ��$ Visible=�6D    5�      �� 5�       6&   �� *	IsFocused+	 false6D   .a�5�      ��  � y�$ 	Collapsed=�6;   5�      �� 5�       6&   �� *IsBarVisible+	 false6G   .a�5�      ��  � ��$ Disabled=�6L   5�      5�      ��5�      �� 5�      ��6   �� *SearchedUsersSection+5�      �� 5�   
   -   5        $ MainScrollViewer��5�   Y   $��4��6   $��1��6$   $��2��64   $��2��6E   .��5�      �� -   5          ��Name$  MainContainer��5�      5�      �� 5�   
   $
 Vertical=�6   $��2��6,   $��3��69   $��3��6I   .+�5�      �� ��6   �� *FriendRequestMessage+$ PropertyChanged=�! �� Mode$! TwoWay=��� 5�      ��6   �� *FriendRequestSection+$ PropertyChanged=�$! TwoWay=��� 5�      ��6   �� *MaybeFriendsMessage+$ PropertyChanged=�$! TwoWay=��� 5�      ��6   �� *MaybeFriendsSection+$ PropertyChanged=�$! TwoWay=�5�      �� 5�   
   $��3��6   $��5��6!   $��Create New Post��6.   $
	 Hand��6j   ��6H   �� *CreateNewPost+6j   5�      5�      