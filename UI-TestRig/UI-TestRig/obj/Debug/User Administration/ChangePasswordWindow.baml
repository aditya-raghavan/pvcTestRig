   M S B A M L   `   `   `  ����E  AUI-TestRig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UI_TestRig
UI_TestRig  5   	   %    UI_TestRig.ChangePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:UI_TestRig     5   	     � 
ResizeMode$  NoResize=�5       � Title$ Change Password��5   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�67   $	��270��6\   $	��350��6i   $��Arial)�6u   
�������.��5	      � .+�5
   
   ��  � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value��%�  '��       'G� �     �� 5      " ��6   .�5      ��  � �� �#ccffff6E    5      P� 5      	 �@J�	IsFocused
 . . �5      ��  � �� �#ffccff6M   5      5      �� 5      " G�6   .�5      ��  � �� �#ccffff6E    5      P� 5      	 �@J�	IsFocused
 . . �5      ��  � �� �#ffccff6M   5      5      5      .+�5      �� -   5         ��Name$ userIdLabel��5      $�� ��6-   $
��Bold"�65   $��	30,10,0,0q�6G   6\   �� 5      -   5        $ newpasswordLabel��5      $��New Password��62   $
��Bold"�6F   $��	30,10,0,0q�6X   6m   G� 5      -   5         G�Name$ newpasswordTextbox��5      $
��Bold"�67   $��5q�6I   $��10(�6U   $��	30,5,30,0q�6c   6�   �� 5       -   5         ��Name$ passwordInvalidLabel��5       
���  ��$��8(�6C   $e��_Password must be 8-16 characters.
Must contain atleast one digit, one uppercase, one lowercase.��6P   $��30,0,0,0q�6�   $y�Hidden=�6�   6�   �� 5"      -   5        $ confirmPasswordLabel��5"      $��Confirm Password��66   $
��Bold"�6N   $��	30,10,0,0q�6`   6u   G� 5#      -   5        $ confirmPasswordTextbox��5#      $
��Bold"�6;   $��5q�6M   $��10(�6Y   $��	30,5,30,0q�6g   6�   �� 5$      -   5        $! confirmPasswordInvalidLabel��5$      $y�Hidden=�69   
���  ��$��8(�6^   $��Passwords do not match.��6k   $��30,0,0,0q�6�   6�   �� 5&      $��6��6   $��2q�6%   ���#4d4d4d$	��100��6O   $��30��6[   $��30,10,50,30q�6g   $
��Left=�6|    5'      ��%^   '��       �� 5(      " ��6   .�5)      ��  �@��CornerRadius$ 7��69   5*      5+      ._�5,      �� -   5         ��Name$ changePasswordButton��5,      $��Change Password��68   $��10(�6R   $
��Bold"�6`   
����������#9999995.      5/      50      51      52      