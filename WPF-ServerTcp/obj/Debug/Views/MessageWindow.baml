   M S B A M L   `   `   `  ????H  DWPF-ServerTcp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:WPF_ServerTcp.ViewsWPF_ServerTcp.Views  5   	   '    !WPF_ServerTcp.Views.MessageWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:WPF_ServerTcp.Views     5   	     ? Title$  MessageWindow??5   	   $	??450??6   $	??500??6,    ? WindowStartupLocation$ CenterScreen=?5	      
?????? $	??800??6F   $	??500??5
   	   .??5      ??  ?? Padding$ 10q?6    ?? CornerRadius$ 	0,0,10,10??5      
???????._?5   
   ? 
??????? ? RowDefinitions 5      ?? 5      $
??0.7* ?6    ?? 5      $??5* ?6    ?? 5      .+?5      ? $??0??6   .+?5      ?? $??Center=?6   $??Center=?68   .+?5      ?? $??30(?6<   ??6    ?? *
ClientName+5      5      5      ?? 5      $??1??6   
???????$	??0.5q?65   ._?5      ? $??1??6   .+?5      ?? .??5       ?? -   5        $??messagePanel??5    %    ?? CanVerticallyScroll . 5"      5#      5$      5%      ?? 5'      $??2??6   
???????$	??0.5q?65   ._?5(      ?  ? ColumnDefinitions 5*      ?? 5+      ?? 5,      $??3* ?6+   ?? 5-      .+?51      ?? $??15,0,8,0q?6    $??Message Section??62   $??Center=?6I   $??9(?6d   $
??Bold"?52       64   ?? 53      $??14(?6   $??1??6.   $??20q?6>   $??45??6J    ?? TextWrapping$
 Wrap=?6Y   " ?? VerticalScrollBarVisibility$
 Auto=?6m   ??54      ?? *ClientMessage+	 ?? Mode$	 TwoWay=?
 ?? UpdateSourceTrigger$
 PropertyChanged=?.??6l    ? 	Resources 55       ?? 
TargetType ?? Property ?? Value??%^   '??       ?? 56      " ??6$   .?57   "   ??  ?@??CornerRadius$ 3??6A   58      59      .??6-   5:      ?? 5<      $??2??6!   $??Center=?61    ?? Orientation$ 
Horizontal=?6L   .+?5=      ?? $??0q?6!   $
??Left=?6,   $??Center=?6G   
?????? $??0q?6{   $??2??6?   ??5>   !   ?? *SendCommand+.??5?      ?? $??25??6$    ?? BitmapScalingMode$ HighQuality=?6/   $??/Images/share.png??6]   6z   5@      ?? 5B      $??14(?6$   $??15,0,0,0q?62   $
??Send??6D   $??Center=?6P   6m   5C      5D      5E      5F      5G      5H      