   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      E AUI-TestRig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UI_TestRig
UI_TestRig 5      0   *UI_TestRig.UserAdministration_UsersTabPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      *localclr-namespace:UI_TestRig     5        N� Title$%  UserAdministration_UsersTabPage��5
      $	��768��6/   $
��1366��6?    N� 
FontFamily$ Arial)�6O    N� 
Background
 ����� � 	Resources 5         DGHeaderStyle �� 
TargetType&   System.Windows.Controls.DataGrid �� Property  DGCHeaderStyle �� Value  DGCCellStyle=  7System.Windows.Controls.Primitives.DataGridColumnHeader*  $System.Windows.Controls.DataGridCell��%�  &	        &	 �     &	 "    �� 5   
   "  6&   .�5      ��  �@ ColumnHeaderStyle# �  62   �� 5       �@ 	CellStyle# �  6*   5      �� 5   
   "  6'   .�5      ��  � ��$ 12(�6)   �� 5       � ��$
 Bold"�6+   6:   �� 5       � ��$
 Left=�6;   6J   5      �� 5   
   "  6%   .�5      ��  � ��
 �����6+   �� 5       � ��
 �   �6+   �� 5       � ��$ 12(�6)   �� 5       � ��$
 Left=�6;   6J   5      5      .��5      �  � RowDefinitions 5    
   �� 5!      $
��auto �6   6,   �� 5"      $
��auto �6   6,   �� 5#      $��* �6   6)   �� 5&      $
��auto �6   6,   .+�5)   
   � -   5        $��TopGrid��5)      $��0��6    � ColumnDefinitions 5*      �� 5+      $
��1.7* �6#   62   �� 5,      $��* �6#   6/   �� 5-      $��* �6#   6/   �� 5.      $
��0.5* �6#   62   .+�51      �� -   5        	 ��Name$	 TitleTextBox��51      ���#cccccc$��Center=�6C   
 �� Padding$

 0,16q�6^   $��0��6m    �� TextAlignment$ Center=�6}   $.��(PhotoVoltaic Junction Box Diode Test Rig��6�   $
��Bold"�6�   $��21(�6�   6�   �� 52      -   5        $��	UserStack��52      $��1��6*   .+�53      �  54      �� 55      $��* �6+   67   �� 56      $��3* �6+   68   .+�58      �� $��0��6   $��2q�6-   ���#4d4d4d._�59      �� -   5        $	 userTextBlock��59   $   $ Right=�6;   $
��User��6Q   $
��Bold"�6]   $��18(�6o   $
 2q�6}   ���#9999996�   5:      �� 5;      $��1��6   $��2q�6-   ���#4d4d4d._�5<      �� -   5         ��Name$ userTextBox��5<   "    �� 
IsReadOnly . $��1��6J   $��14(�6Z   $
��Bold"�6h   $��3q�6z   6�   5=      5?      � 5@       5A      �� 5B      $
��80.5 �6+   6:   �� 5C      $��3* �6+   68   �� 5D      $
��0.5* �6+   6:   �� 5E      $��* �6+   67   .+�5J      �� $��0��6   $��2q�6-   ���#4d4d4d._�5K      �� -   5        $	 programTextBlock��5K   $   $ Right=�6>   $��Program��6T   $
��Bold"�6c   $��18(�6u   $
 2q�6�   ���#9999996�   5L      �� 5M      $��1��6   $��2q�6-   ���#4d4d4d._�5N      �� -   5        $ programTextBox��5N   "   $��1��6:   $��3q�6J   6X   5O      �� 5P      $��2��6   $��2q�6-   ���#4d4d4d._�5Q      �� -   5        $	 nrTextBlock��5Q   $   $ Right=�69   $��Nr��6O   $
��Bold"�6Y   $��18(�6k   $
 2q�6y   ���#9999996�   5R      �� 5S      $��4��6   $��2q�6-   ���#4d4d4d._�5T      �� -	   5        $ 	nrTextBox��5T   "   $��1��65   $��3q�6E   6S   5U      5W      5X      �� 5[      -
   5        $��	ModeStack��5[      $��2��6*   .+�5\      �  5]      �� 5^      $��* �6+   67   �� 5_      $��3* �6+   68   .+�5c      �� $��0��6   $��2q�6-   ���#4d4d4d._�5d      �� -   5        $	 modeTextBlock��5d   $   $ Right=�6;   $
��Mode��6Q   $
��Bold"�6]   $��18(�6o   $
 2q�6}   ���#9999996�   5e      �� 5f      $��1��6   $��2q�6-   ���#4d4d4d._�5g      �� -   5        $	 autoTextBlock��5g   $   $
��AUTO��6;   $ Center=�6G   $
 2q�6^   $��18(�6j   $
��Bold"�6x   ���#66ffff6�   5h      5j      � 5l       5m      �� 5n      $��* �6+   67   �� 5o      $��3* �6+   68   .+�5s      �� $��0��6   $��2q�6-   ���#4d4d4d._�5t      �� -   5        $	 statusTextBlock��5t   $   $ Right=�6=   $��Status��6S   $
��Bold"�6a   $��18(�6s   $
 2q�6�   ���#9999996�   5u      �� 5v      -   5         ��Name$ autoRunningTextBlock��5v      $��1��6;   $��2q�6K   ���#4d4d4d._�5w      �� -   5        $	 autoRunningTextBlock1��5w   $   $��AUTO RUNNING��6C   
���  ��$ Center=�6h   $
 2q�6   $��18(�6�   $
��Bold"�6�   6�   5x      5z      5|      �� 5~      $��3��6   .+�5      �� $��2q�6   ���#4d4d4d._�5�      �� -   5        $	 dateTextBlock��5�       
�������
���   �$ Center=�6�   $
 2q�6�   $��18(�6�   $
��Bold"�6�   ��67   ��  �� Today �� Source# Z  �� StringFormat$ {0:dd-MM-yyyy}��6�   5�      �� 5�      $��2q�6   ���#4d4d4d._�5�      �� -   5        $	 timeTextBlock��5�       
�������
���   �$ Center=�5�   [   $
 2q�6r   $��18(�6~   $
��Bold"�6�   ��5�   7   �� 
 �� Now# Z $ {0: hh:mm:ss}��5�   �   5�      5�      5�      �� 5�   
   $��2q�6   ���#4d4d4d$��1��6<   ._�5�      �� -   5        $	 screenNameTextBlock��5�      $��User Administration��65   $
 5q�6P   $��14(�6\   $
��Bold"�6j   
����������#ff66006�   5�      �� 5�   
   -   5         ��Name$ 
tabControl��5�       �� TabStripPlacement$	 Top=�6)   $��2��6A   $��0,20,0,0q�6N   .@�5�      ��  �� 
IsSelected . ��5�      �� 5�      $��Users��6    $��16(�6-   $
 5q�6;   6I   .��5�      � -   5         �Name$ usersParentGrid��5�       5�      �� 5�      $
��auto �6+   6:   �� 5�      $��* �6+   67   �� 5�      $
��auto �6+   6:    5�      �� 5�      $��* �6(   65   .+�5�        -   5          Name$ usersDataGrid��5�      $��0��66      CanUserAddRows . $��14(�6Y      RowHeaderWidth$ 0Q�6g      SelectionMode$ Single=�6z      CanUserSortColumns .  #���   6�      AutoGenerateColumns .  $��1��6
  ��6�   ��    Columns 5�      0  *System.Windows.Controls.DataGridTextColumn  5�      ,  &System.Windows.Controls.DataGridColumn   Header$ User ID��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   ��   �� Path$  UserId�! �� UpdateSourceTrigger$! PropertyChanged=�6�   4  .System.Windows.Controls.DataGridTemplateColumn  5�      $ Password��65   "   CellTemplate" 5�   "   �� 5�   &   .R�5�   *   �� ��64   �� $  PasswordEncrypted�$! PropertyChanged=�6�   5�   '   #   CellEditingTemplate# 5�   "   �� 5�   &   .R�5�   *   �� ��64   �� $  PasswordEncrypted�$! PropertyChanged=�6�   5�   '   5�        5�      $ Group��65   -   5        groupDataColumn��" 5�   "   �� 5�   &   .R�5�   *   �� $ �� DisplayMemberPath$$ 	GroupName��6[   ��63   �� $  
GroupsList�{�6y   �� $  CanChangeGroup�% �� SelectedItem% 6�   �� $  Group�$! PropertyChanged=�5�   '   # 5�   "   �� 5�   &   .R�5�   *   �� $$ 	GroupName��6[   ��63   �� $  
GroupsList�{�6y   �� $  CanChangeGroup�% 6�   �� $  Group�$! PropertyChanged=�5�   '   5�      & � ContextMenu& 5�      �� 5�      -   5        ' ��Name$' contextMenu��5�   *   .@�5�   "   m� -   5        ( m�Name$( deleteContextMenu��5�   +   $��Delete User��6F   6[   5�       5�      ) �� Triggers* P� Property+ P� Value)  #System.Windows.Controls.DataGridRow��%  '        '  �     �� 5�      "  6$   ) 5�   "   P� 5�   &   * �@ 
IsSelected+ . . �5�   *   ��  � ��
 �����6G   �� 5�   *    � ��
 �   �6W   5�   '   .�5�   "   -   5        5�      �� 5�      "  6$   .�5�   "   -   5        5�      5�      ,   	CellStyle, 5�      �� 5�      "  6$   ) 5�   "   P� 5�   &   * �@ 
IsSelected+ . . �5�   *   ��  � ��
 �����6G   �� 5�   *    � ��
 �   �6G   �� 5�   *    � ��
 ��  �6H   �� 5�   *    � ��$ 0q�6L   6X   5�   '   P� 5�   &   * �@J�IsKeyboardFocusWithin+ . . �5�   *   ��  � �� �#00a2ed6Y   �� 5�   *    � ��
 �����6W   5�   '   .�5�   "   -   5        5�      5�      5�      5�      �� 5�      ��5�      �� 5�      $��Groups��6    $��16(�6.   $
 5q�6<   6J   .��5�      �  5�      �� 5�      $
��auto �6+   6:   �� 5�      $	��29* �6+   69   �� 5�      $��1331* �6+   �� 5�      $
��auto �6+   6:   .+�5�        -   5        $ groupsDataGrid��5�      $��14(�67   $ Single=�6E   $ 0Q�6t   #���   6�   -   
IsReadOnly- .  .  $��1��6�   $��3��6�   $��0,0,-0.2,-0.4q�6  ��6\   �� , 5�      �� 5�      "  6$   ) 5�   "   P� 5�   &   * �@ 
IsSelected+ . . �5�   *   ��  � ��
 �����6G   �� 5�   *    � ��
 �   �6G   �� 5   *    � ��
 ��  �6H   �� 5  *    � ��$ 0q�6L   6X   5  '   P� 5  &   * �@J�IsKeyboardFocusWithin+ . . �5  *   ��  � �� �#00a2ed6Y   �� 5  *    � ��
 �����6W   �� 5  *    � ��
 ��  �6H   �� 5  *    � ��$ 0q�6L   6X   5  '   .�5
  "   -   5        5     5     5     5     5     � 5  
   $��4��6    5     �� 5     $��* �6#   6/   �� 5     $
��auto �6#   62   �� 5     $��* �6#   6/   5     � 5'  
   $��3��6    5(     ��%�   '��       '�� Y     �� 5)     " ��6   .�5*     ��  �@��CornerRadius$ 7��65   6A   5+     �� 5,     " ��6   .�5-     ��  � ��$
 44.8��6/   6>   5.     5/      50     �� 51     6%   �� 52     6%   �� 53     6%   �� 54     6%   �� 55     6%   �� 56     6%   �� 57     6%   �� 58     6%   �� 59     6%   �� 5:     6%   .+�5=     �� $��0��6   . �� Padding$. 0q�6%   $��2q�61   ���#4d4d4d._�5>     �� $��18(�6   $
��Bold"�6)   
����������#999999 5?     ��%^   '��       �� 5@     " ��6    .�5A     ��  �@��CornerRadius$ 7��6=   5B     5C     5D     5E     �� 5G     $��1��6   $��2q�6%   ���#4d4d4d._�5H     �� $��18(�6   $
��Bold"�6)   
����������#999999$	��0,0q�6c    5I     ��%^   '��       �� 5J     " ��6    .�5K     ��  �@��CornerRadius$ 7��6=   5L     5M     5N     5O     �� 5Q     $��2��6   $��2q�6%   ���#4d4d4d._�5R     �� $��18(�6   $
��Bold"�6(   
����������#999999$	��0,0q�6b    5S     ��%^   '��       �� 5T     " ��6    .�5U     ��  �@��CornerRadius$ 7��6=   5V     5W     5X     5Y     �� 5[     $��3��6   $��2q�6%   ���#4d4d4d._�5\     �� $��3q�6   $��18(�6'   $
��Bold"�65   
����������#999999$	��0,0q�6o    5]     ��%^   '��       �� 5^     " ��6    .�5_     ��  �@��CornerRadius$ 7��6=   5`     5a     5b     5c     �� 5e     $��4��6   $��2q�6%   ���#4d4d4d._�5f     �� $��18(�6   $
��Bold"�6(   
����������#999999$	��0,0q�6b    5g     ��%^   '��       �� 5h     " ��6    .�5i     ��  �@��CornerRadius$ 7��6=   5j     5k     5l     5m     �� 5o     $��5��6   $��2q�6%   ���#4d4d4d._�5p     �� $��18(�6   $
��Bold"�6)   
����������#999999$	��0,0q�6c    5q     ��%^   '��       �� 5r     " ��6    .�5s     ��  �@��CornerRadius$ 7��6=   5t     5u     5v     5w     �� 5y     $��6��6   $��2q�6%   ���#4d4d4d._�5z     �� $��18(�6   $
��Bold"�6*   
����������#999999$	��0,0q�6d    5{     ��%^   '��       �� 5|     " ��6    .�5}     ��  �@��CornerRadius$ 7��6=   5~     5     5�     5�     �� 5�     $��7��6   $��2q�6%   ���#4d4d4d._�5�     �� -   5        / ��Name$/ addUserButton��5�     $��18(�61   $
��Bold"�6?   
����������#999999$	��0,0q�6y    5�     ��%^   '��       �� 5�     " ��6    .�5�     ��  �@��CornerRadius$ 7��6=   5�     5�     5�     5�     �� 5�     $��8��6   $��2q�6%   ���#4d4d4d._�5�     �� -   5        $/ 
saveButton��5�     $��18(�6.   $
��Bold"�6<   
����������#999999$	��0,0q�6v    5�     ��%^   '��       �� 5�     " ��6    .�5�     ��  �@��CornerRadius$ 7��6=   5�     5�     5�     5�     �� 5�     $��9��6   $��2q�6%   ���#4d4d4d._�5�     �� -    5        $/ 
exitButton��5�     $
��Back��6.   $��18(�6=   $
��Bold"�6K   
����������#999999$��0.4,0,0.2,0.4q�6�   $��Bottom=�6�    5�     ��%^   '��       �� 5�     " ��6    .�5�     ��  �@��CornerRadius$ 7��6=   5�     5�     5�     5�     5�     5�     5�     