   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  E AUI-TestRig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:UI_TestRig
UI_TestRig    UI_TestRig.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6sys&clr-namespace:System;assembly=mscorlib     *localclr-namespace:UI_TestRig       � Title$  
MainWindow��$	��864��$
��1536��.���� -   $��	MainStack��.+�� -   $��TopGrid�� � ColumnDefinitions �� $
��1.7* ��� $��* ��� $��* ��� $
��0.5* �.+��� -   $��	TitleText�����#cccccc$��Center=� �� Padding$
 0,16q�$��0�� �� TextAlignment$ Center=�$.��(PhotoVoltaic Junction Box Diode Test Rig��$
��Bold"�$��25(��� -   $��	UserStack��$��1��.+��  �� $��* ��� $��3* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$
��User��$
��Bold"�$��18(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -    ��Name$ userText��$��1��$��3q��  �� $
��90.5 ��� $��3* ��� $
��0.5* ��� $��* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��Program��$
��Bold"�$��18(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� $��1��$��3q��� $��2��$��2q����#4d4d4d._��� $ Right=�$��Nr��$
��Bold"�$��18(�$ 2q����#999999�� $��4��$��2q����#4d4d4d._��� $��1��$��3q��� -   $��	ModeStack��$��2��.+��  �� $��* ��� $��3* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$
��Mode��$
��Bold"�$��18(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� $
��AUTO��$ Center=�$ 2q�$��18(�$
��Bold"����#66ffff�  �� $��* ��� $��3* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��Status��$
��Bold"�$��18(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� $��AUTO RUNNING��
���  ��$ Center=�$ 2q�$��18(�$
��Bold"��� $��3��.+��� $��2q����#4d4d4d._��� 
�������
���   �$ Center=�$ 2q�$��18(�$
��Bold"�����  �� Today �� Source# Z  �� StringFormat$ {0:dd-MM-yyyy}���� $��2q����#4d4d4d._��� 
�������
���   �$ Center=�$ 2q�$��18(�$
��Bold"����� 
 �� Now# Z $ {0: hh:mm:ss}���� $��2q����#4d4d4d._��� $��Main Screen��$ 5q�$��14(�$
��Bold"�
����������#ff6600�� -   $��MainContentBorder��	 �� Padding$	 5q�._��  �� $	��10* ��� $	��16* ��� $	��10* �.+��� $	 2q�$��2q�$��0����!� 
 !� Visual
 ��  �� StrokeDashArray$	 4 2R�
�������$��1������  �� RelativeSource ��  �� AncestorType" �� �� Path$ ActualWidth�����  �� " ��$ ActualHeight�._��� .+��� $��Test Information��$ 10q�$��14(�$ Center=�$
��Bold"�
����������#ff8533�� ���#cccccc.+��� -   $��	ComboBox1��$	��370��$��30��$��Center=�$��
10,10,10,0q�.@���  �� 
IsSelected . $��	TREY 1256���� $��	TREY 1256���� $��	TREY 1256���� $��	TREY 1256���� $��	TREY 1256���� $��	TREY 1256���� $��
10,30,0,10q�$��Comparison Completed��
���  ��$��16(��� $��50��$��Select Model��$��18(�$
��Bold"�
����������#999999$	��0,3q� � 	Resources  �� 
TargetType �� Property �� Value��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� .+��  �� $��* ��� $��2* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��Operator��$
��Bold"�$��16(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -	   $ operatorText��$��1��$��3q��  �� $��* ��� $��2* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��
Diode Code��$
��Bold"�$��16(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -
   $ diodecodeText��$��1��$��3q��  �� $��* ��� $��2* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��Customer Code��$
��Bold"�$��16(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -   $ customercodeText��$��1��$��3q��  �� $��* ��� $��2* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��Additional Code��$
��Bold"�$��16(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -   $ additionalcodeText��$��1��$��3q��  �� $��* ��� $��2* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��JB Serial Nr��$
��Bold"�$��16(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -   $ jbserialText��$��1��$��3q��  �� $��* ��� $��2* �.+��� $��0��$��2q����#4d4d4d._��� $ Right=�$��Batch Number��$
��Bold"�$��16(�$ 2q����#999999�� $��1��$��2q����#4d4d4d._��� -   $ batchnoText��$��1��$��3q��� $	��100���� $��2q����#4d4d4d$��0,0,0,5q�._��� $
��Mode��$ 7q�$��14(�$ Center=�$
��Bold"�
����������#ff8533�� $��2q����#4d4d4d._��� $
��Auto��$ 7q�$��14(�$ Center=�$
��Bold"�
����������#99ffcc�  �� $��* ��� $��* �.+��� $��0��$��50��$��Manual��$��18(�$
��Bold"�
����������#999999$	��3,3q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� -   $��1��$��50��$
��Auto��$��18(�$
��Bold"�
����������#999999$	��3,3q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $	 2q�$��2q�$��2����!� 
 �� $	 4 2R�
�������$��1������  �� " ��$ ActualWidth�����  �� " ��$ ActualHeight�._��� .+��� $��	Test Data��$ 10q�$��14(�$ Center=�$
��Bold"�
����������#ff8533$��0,0,0,7q��  � RowDefinitions �� �� �� �� �� �� �� �� �� ��  �� $
��1.3* ��� $��* ��� $��* ��� $��* ��� $��* ��� $
��1.1* �.+��� $��0��$��0��$��2q����#4d4d4d._��� $ Center=�$
��Item��$
��Bold"�$��16(�$ 2q����#cccccc�� $��1��$��0��$��2q����#4d4d4d._��� $ Center=�$��Units��$
��Bold"�$��16(�$ 2q����#cccccc�� $��2��$��0��$��2q����#4d4d4d._��� $ Center=�$	��Low��$
��Bold"�$��16(�$ 2q����#cccccc�� $��3��$��0��$��2q����#4d4d4d._��� $ Center=�$��Actual��$
��Bold"�$��16(�$ 2q����#cccccc�� $��4��$��0��$��2q����#4d4d4d._��� $ Center=�$
��High��$
��Bold"�$��16(�$ 2q����#cccccc�� $��5��$��0��$��2q����#4d4d4d._��� $ Center=�$��Status��$
��Bold"�$��16(�$ 2q����#cccccc�� $��0��$��1��$��2q����#4d4d4d._��� $ Center=�$��Fwd Current��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��1��$��2q����#4d4d4d._��� $ Center=�$
��Amps��$
��Bold"�$��14(�$ 2q��� $��2��$��1��$��2q����#4d4d4d._��� $ Center=�$
��+6.0��$
��Bold"�$��14(�$ 2q��� $��3��$��1��$��2q����#4d4d4d._��� $ Center=�$
��+0.0��$
��Bold"�$��14(�$ 2q�
����  ��� $��4��$��1��$��2q����#4d4d4d._��� $ Center=�$
��+6.3��$
��Bold"�$��14(�$ 2q��� $��5��$��1��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $��0��$��2��$��2q����#4d4d4d._��� $ Center=�$��Fwd Voltage��$
��Bold"�$��14(�$ 2q����#cccccc$��0.4,0.4,0,0q��� $��1��$��2��$��2q����#4d4d4d._��� $ Center=�$��Volts��$
��Bold"�$��14(�$ 2q��� $��2��$��2��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�1.3���� $��3��$��2��$��2q����#4d4d4d._��� $ Center=�$
��+0.0��$
��Bold"�$��14(�$ 2q�
����  ��� $��4��$��2��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�2.7���� $��5��$��2��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $��0��$��3��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q����#cccccc$��
Fwd Drop 1���� $��1��$��3��$��2q����#4d4d4d._��� $ Center=�$��mV��$
��Bold"�$��14(�$ 2q��� $��2��$��3��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�300���� $��3��$��3��$��2q����#4d4d4d._��� $ Center=�$��+0��$
��Bold"�$��14(�$ 2q�
����  ��� $��4��$��3��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�500���� $��5��$��3��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $��0��$��4��$��2q����#4d4d4d._��� $ Center=�$��
Fwd Drop 2��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��4��$��2q����#4d4d4d._��� $ Center=�$��mV��$
��Bold"�$��14(�$ 2q��� $��2��$��4��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�300���� $��3��$��4��$��2q����#4d4d4d._��� $ Center=�$��+0��$
��Bold"�$��14(�$ 2q�
����  ��� $��4��$��4��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�500���� $��5��$��4��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $��0��$��5��$��2q����#4d4d4d._��� $ Center=�$��
Fwd Drop 3��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��5��$��2q����#4d4d4d._��� $ Center=�$��mV��$
��Bold"�$��14(�$ 2q��� $��2��$��5��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�300���� $��3��$��5��$��2q����#4d4d4d._��� $ Center=�$��+0��$
��Bold"�$��14(�$ 2q�
����  ��� $��4��$��5��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $	�500���� $��5��$��5��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $��0��$��6��$��2q����#4d4d4d._��� $ Center=�$��
Total Drop��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��6��$��2q����#4d4d4d._��� $ Center=�$��mV��$
��Bold"�$��14(�$ 2q��� $��3��$��6��$��2q����#4d4d4d._��� $ Center=�$��+0��$
��Bold"�$��14(�$ 2q�
����  ��� $��0��$��7��$��2q����#4d4d4d._��� $ Center=�$��	Contact R��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��7��$��2q����#4d4d4d._��� $ Center=�$
��Ohms��$
��Bold"�$��14(�$ 2q��� $��2��$��7��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $�0.000���� $��3��$��7��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q�
����  ��� $
�+0.0���� $�00���� $��4��$��7��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $�0.350���� $��5��$��7��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $��0��$��8��$��2q����#4d4d4d._��� $ Center=�$��Rev Volt��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��8��$��2q����#4d4d4d._��� $ Center=�$��Volts��$
��Bold"�$��14(�$ 2q��� $��3��$��8��$��2q����#4d4d4d._��� $ Center=�$
��+0.0��$
��Bold"�$��14(�$ 2q�
����  ��� $��0��$��9��$��2q����#4d4d4d._��� $ Center=�$��Rev Current��$
��Bold"�$��14(�$ 2q����#cccccc�� $��1��$��9��$��2q����#4d4d4d._��� $ Center=�$��uAmps��$
��Bold"�$��14(�$ 2q��� $��2��$��9��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $�0���� $��3��$��9��$��2q����#4d4d4d._��� $ Center=�$
��+0.0��$
��Bold"�$��14(�$ 2q�
����  ��� $��4��$��9��$��2q����#4d4d4d._��� $ Center=�$
��Bold"�$��14(�$ 2q��� $�+���� $�25���� $��5��$��9��$��2q����#4d4d4d._��� $ Center=�$��PENDNG��$
��Bold"�$��14(�$ 2q����#cccccc�� $	 2q�$��2q�$��1����!� 
 �� $	 4 2R�
�������$��1������  �� " ��$ ActualWidth�����  �� " ��$ ActualHeight�._��� .+��� $��
Rig Status��$ 10q�$��14(�$ Center=�$
��Bold"�
����������#ff8533�  �� �� �� .+�� $��0��$	��5,0q� �� $��* ��� $
��0.7* ��� $
��0.3* �.+��� $��0��$��2q����#4d4d4d._��� $��Drop Voltage 1��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��1��$��2q����#4d4d4d._��� $��+0�����#0033cc$��Center=�$ Center=�$ 2q�$��24(�$
��Bold"��� $��2��$��2q����#4d4d4d._��� $��mV��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"�� $��1��$	��5,0q� �� $��* ��� $
��0.7* ��� $
��0.3* �.+��� $��0��$��2q����#4d4d4d._��� $��Drop Voltage 2��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��1��$��2q����#4d4d4d._��� $��+0�����#0033cc$��Center=�$ Center=�$ 2q�$��24(�$
��Bold"��� $��2��$��2q����#4d4d4d._��� $��mV��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"�� $��2��$	��5,0q� �� $��* ��� $
��0.7* ��� $
��0.3* �.+��� $��0��$��2q����#4d4d4d._��� $��Drop Voltage 3��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��1��$��2q����#4d4d4d._��� $��+0�����#0033cc$��Center=�$ Center=�$ 2q�$��24(�$
��Bold"��� $��2��$��2q����#4d4d4d._��� $��mV��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"�� $	��345��$��10,0,10.2,0q�����  �� ImageSource$ images/background.jpg��.+�� $��199,104,250,210q� �� $��* ��� $
��0.7* ��� $
��0.3* �.+��� $��0��$��2q����#4d4d4d._��� $��
Total Drop��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��1��$��2q����#4d4d4d._��� $��-4�����#0033cc$��Center=�$ Center=�$ 2q�$��24(�$
��Bold"��� $��2��$��2q����#4d4d4d._��� $��mV��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"�� $��98,152,107,77q� �� $��* ��� $��3* ��� $��3* ��� $
��0.8* � �� $��* ��� $��3* ��� $��3* �.+��� $��1��$��0��$��2q����#4d4d4d$��0.2,-7,0.2,49.4q�$��2��._��� $	��Set��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"�$��25��$��0.4,-7.6,0,-4.6q��� $��1��$��0��$��2q����#4d4d4d$��3��$��170.2,-7,45.4,49.4q�$��2��._��� $��Actual��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��0��$��1��$��2q����#4d4d4d._��� $��Current��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��1��$��1��$��2q����#4d4d4d._��� $	��6.0�����#0033cc �� TextAlignment$ Center=�$��2q�$��30(�$
��Bold"�$��46��$��0.4,-1.6,0,-2.8q��� $��2��$��1��$��2q����#4d4d4d._��� $
��-0.0�����#0033cc$ Center=�$ 2q�$��30(�$
��Bold"�$��44��$��0.4,-1.6,0,-2.8q��� $��3��$��2q����#4d4d4d$��0.2,2.2,0.4,0.4q�$��1��._��� $��A��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��0��$��2��$��2q����#4d4d4d._��� $��Voltage��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��1��$��2��$��2q����#4d4d4d._��� $	��6.0�����#0033cc$ Center=�$��2q�$��30(�$
��Bold"�$��46��$��0.4,-1.6,0,-2.8q��� $��2��$��2��$��2q����#4d4d4d._��� $	��2.8�����#0033cc$ Center=�$ 2q�$��30(�$
��Bold"�$��44��$��0.4,-1.6,0,-2.8q��� $��3��$��2��$��2q����#4d4d4d._��� $��V��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"�� $	��300�� �� $��* ��� $
��0.7* ��� $
��0.3* �.+��� $��0��$��2q����#4d4d4d._��� $��Actual Reverse Current��
���   �$��Center=�$ Center=�$ 2q�$��12(�$
��Bold"��� $��1��$��2q����#4d4d4d._��� $��-0��
���**��$��Center=�$ Center=�$ 2q�$��24(�$
��Bold"��� $��2��$��2q����#4d4d4d._��� $��uA��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��� $��2q����#4d4d4d._��� $��Automatic Test Running��$ Center=�
���   �$ 5q�$��24(�$
��Bold"����#00ff00�  �� �� .+��� $��0��$��2q����#4d4d4d._��� $
��PASS��$ Center=����#666666$ 5q�$��24(�$
��Bold"����#cccccc�� $��1��$��2q����#4d4d4d._��� $
��FAIL��$ Center=����#666666$ 5q�$��24(�$
��Bold"����#cccccc� $	��200��$��Right=� �� $��* ��� $
��0.7* ��� $
��0.3* �.+��� $��0��$��2q����#4d4d4d._��� $��
Cycle Time��
���   �$��Center=�$ Center=�$ 2q�$��12(�$
��Bold"��� $��1��$��2q����#4d4d4d._��� $	��4.3��
���**��$��Center=�$ Center=�$ 2q�$��24(�$
��Bold"��� $��2��$��2q����#4d4d4d._��� $��uA��
���   �$��Center=�$ Center=�$ 2q�$��14(�$
��Bold"��  �� �� �� �� �� �� �� �� �� �� .+��� $��0��$	 0q�$��2q����#4d4d4d._��� -   $��50��$��F1��$��18(�$
��Bold"�
����������#999999$��0.4,0.4,0,-0.8q�$
��Left=�$	��148�� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��1��$��2q����#4d4d4d._��� -   $��50��$��F2��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��2��$��2q����#4d4d4d._��� $��50��$��	Data Logs��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��3��$��2q����#4d4d4d._��� $��50��$!��Running Process
 Parameter��$��16(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��4��$��2q����#4d4d4d._��� $��50��$��Alarms��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��5��$��2q����#4d4d4d._��� $��50��$��Edit Process 
Parameter��$��16(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��6��$��2q����#4d4d4d._��� $��50��$��Log Out��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��7��$��2q����#4d4d4d._��� $��50��$
��Help��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��8��$��2q����#4d4d4d._��� $��50��$��Diagnostics��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7���� $��9��$��2q����#4d4d4d._��� $��50��$
��Exit��$��18(�$
��Bold"�
����������#999999$	��0,0q� ��%9   '��       �� " ��.���  �@��CornerRadius$ 7��