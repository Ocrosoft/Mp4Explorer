   M S B A M L   `   `   `  ����J  FMp4Explorer, Version=1.0.1.40086, Culture=neutral, PublicKeyToken=null)clr-namespace:Mp4ExplorerMp4Explorer  5          Mp4Explorer.Shell   5   	   R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      e aMicrosoft.Practices.Composite.Presentation, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null.cal$http://www.codeplex.com/CompositeWPF  5      #appclr-namespace:Mp4Explorer  5        � Title$  Mp4 Explorer��5      $	��600��5      $	��800��6    � Icon$$ /Mp4Explorer;component/App.ico��5       � WindowStartupLocation$ CenterScreen=�5	       � 	Resources 5
         	imageOpen  imageSmoothStreaming  imageFov  imageUploadToAzure��%�  &	        &	 _     &	 �     &	 (    �� 5   
   $/��)/Mp4Explorer;component/Images/open_16.ico��6"   $��16��6U   $��16��6a   �� 5   
   $;��5/Mp4Explorer;component/Images/smooth_streaming_16.png��6-   $��16��6l   $��16��6x   �� 5   
   $.��(/Mp4Explorer;component/Images/fov_16.png��6!   $��16��6S   $��16��6_   �� 5   
   $:��4/Mp4Explorer;component/Images/upload_to_azure_16.png��6+   $��16��6i   $��16��6u   5      .��5      ]� .+�5   
   �� $	��Top=�6   ._�5      o� -   5        $��menuMain��5      $��22��6   .@�5      m� $
��File��6   .@�5      m� $
��Open��6       Mp4Explorer.GlobalCommands   OpenCommand m� Command# Z 6-    m� Icon# [  6a   �� 5      m� 5      -   5        $��menuItemExit��5   -   $
��Exit��6   6A   5      m� 5      $��Tools��6   .@�5      m� -   5        $��menuItemConvertToMp4��5      $$��Conver video to MP4 (H264+AAC)��6;   {�.  6�   m� 5      $��Smooth Streaming��6   # [ 69   .@�5      m� -   5        $��menuItemCreateFixedOffset��5   #   $��Create Fixed Offset��6D   # [ 6�   m� 5      -   5        $��menuItemUploadToAzure��5   #   $��Upload to Azure��6@   # [ 6}   5      5      m� 5      $
��Help��6   .@�5       m� -   5        $��menuItemAbout��5       $��About Mp4 Explorer��64   6P   5!      5"      5#      �� 5$   
   $	��Top=�6   ._�5%      c� .@�5&      �� #��Z 6   .��5'      �� $/��)/Mp4Explorer;component/Images/open_16.ico��6   $��16��6O    � ToolTip$
 Open��6[   5(      �� 5)      �� 5*      -   5        .��5+      �� $.��(/Mp4Explorer;component/Images/fov_16.png��6   $��16��6N   $ Create Fixed Offset��6Z   5,      �� 5-      -   5        .��5.      �� $:��4/Mp4Explorer;component/Images/upload_to_azure_16.png��6   $��16��6Z   $ Upload to Azure��6f   5/      50      51      �� 52   
   $��Bottom=�6   ._�53      �� .@�54      �� -	   5        $��statusBarItemPath��54       $�� ��69   55      56      �� 57   
   ._�58      �  � ColumnDefinitions 59      �� 5:      $	��200 �6'   �� 5;      $
��Auto �6'   �� 5<      .+�5>      �� -
   5        $��
LeftRegion��5>   1   $��0��6!   F  @Microsoft.Practices.Composite.Presentation.Regions.RegionManager	   
RegionName$	 
LeftRegion��6C   �� 5?      $��1��6   $��Center=�6/   $��Stretch=�6L   
 �� ShowsPreview
 . $��4��6|   �� 5@      $��2��6   $��1q�6)   
�������._�5A      �� -   5        $��
MainRegion��5A   %   $	 
MainRegion��67   5B      5C      5D      5E      5F      