   M S B A M L   `   `   `  ����  Mp4Explorer    Mp4Explorer.ErrorWindow   5   	   R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5        � SizeToContent$  WidthAndHeight=�5       � Icon$0 */Mp4Explorer;component/Images/error_16.png��5       � Title$ Error��5       � WindowStartupLocation$ CenterScreen=�5       � ShowInTaskbar .   � 
ResizeMode$ NoResize=�5	      .��5
      � -   5         �Name$ 
LayoutRoot��5
      $	��405��6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ IntroductoryText��5      $��0��6.   $��4,4,4,4q�6;   $Z��TAn unknown error was encountered. Please contact your provider for more information.��5      $	��400��6l   $
��Wrap=�6x   $
��Left=�6�   �� 5   
   -   5        	 ��Name$	 ContentStackPanel��5      $��2��60   $��4,6,4,0q�6=   .+�5      �� -   5        $ 	LabelText��5      $
��Wrap=�6+   $��0,0,0,2q�6?   $��Error details��5      �� 5      -   5        
 ��Name$
 ErrorTextBox��5      $��90��6,    �� TextWrapping$
 Wrap=�68    �� 
IsReadOnly . " �� VerticalScrollBarVisibility$
 Auto=�5      5      �� 5   
   -   5        $��buttonOK��5      $��3��6!   $��75��5      $��23��6   $��Right=�6$   $��0,10,4,4q�6@   $��0��5      $��OK��6   5      5      