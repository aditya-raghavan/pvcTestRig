   M S B A M L   `   `   `  ����E  AUI-TestRig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UI_TestRig
UI_TestRig  5   	       UI_TestRig.LoginPage   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:UI_TestRig     5   	   -   5          � 
ResizeMode$  NoResize=�5       � Title$ 
User Login��5   	    � ShowInTaskbar .   � WindowStartupLocation$ CenterScreen=�62   $	��270��6W   $	��400��6d   $��Arial)�6p   
�������.��5	      ��  � 	Resources 5
   
    �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value��%�  '��       'G� �     �� 5      " ��6   .�5      ��  � �� �#ccffff6A    5      P� 5      	 �@J�	IsFocused
 . . �5      ��  � �� �#ffccff6I   5      5      �� 5      " G�6   .�5      ��  � �� �#ccffff6A    5      P� 5      	 �@J�	IsFocused
 . . �5      ��  � �� �#ffccff6I   5      5      5      .+�5   
   �� -   5         ��Name$ userIdLabel��5      $��User ID��6)   $
��Bold"�68   $��	30,20,0,0q�6J   6_   �� 5   
   -   5         ��Name$ userIdTextbox��5      $
��Bold"�6*   $��10q�6<   $��14(�6I   $��	30,5,30,0q�6W   6�   �� 5   
   -   5        $ passwordLabel��5      $��Password��6+   $
��Bold"�6;   $��	30,15,0,0q�6M   6b   G� 5   
   -   5         G�Name$ passwordTextbox��5      $
��Bold"�60   $��10q�6B   $��14(�6O   $��	30,5,30,0q�6]   6�   �� 5    
   -   5        $��loginFailedLabel��5    #   $��	30,5,30,0q�6   $
��Bold"�6;   
���  ��$#��Incorrect User ID or Password��6^   $��10(�6�   $y�Hidden=�6�   6�   �� 5!   
   $��6��6   $��2q�6!   ���#4d4d4d$	��100��6K   $��30��6W   $��30,10,30,30q�6c   $
��Left=�6x    5"      ��%^   '��       �� 5#      " ��6   .�5$      ��  �@��CornerRadius$ 7��65   5%      5&      ._�5'      �� -   5         ��Name$ logInButton��5'      $��Log In��6+   $��14(�6<   $
��Bold"�6J   
����������#9999995)      5*      5+      5,      