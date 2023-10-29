using Mouse_Shop.Model;
using Mouse_Shop.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace Mouse_Shop.Services.Classes
{
    internal class PurchaseService : IPurchaseService
    {
        public WebClient WebClient { get; set; } = new();
        public void GenerateReciept(ObservableCollection<Product> Products, float subtotal)
        {
            string i = "0";
            if (File.Exists("last.txt"))
                i = File.ReadAllText("last.txt");
            MailMessage mail = new();
            mail.Body = $@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
<html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"" style=""font-family:arial, 'helvetica neue', helvetica, sans-serif"">
<head>
<meta charset=""UTF-8"">
<meta content=""width=device-width, initial-scale=1"" name=""viewport"">
<meta name=""x-apple-disable-message-reformatting"">
<meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
<meta content=""telephone=no"" name=""format-detection"">
<title>New message</title><!--[if (mso 16)]>
<style type=""text/css"">
a {{text-decoration: none;}}
</style>
<![endif]--><!--[if gte mso 9]><style>sup {{ font-size: 100% !important; }}</style><![endif]--><!--[if gte mso 9]>
<xml>
<o:OfficeDocumentSettings>
<o:AllowPNG></o:AllowPNG>
<o:PixelsPerInch>96</o:PixelsPerInch>
</o:OfficeDocumentSettings>
</xml>
<![endif]--><!--[if !mso]><!-- -->
<link href=""https://fonts.googleapis.com/css2?family=Sora&display=swap"" rel=""stylesheet""><!--<![endif]-->
<style type=""text/css"">
#outlook a {{
padding:0;
}}
.es-button {{
mso-style-priority:100!important;
text-decoration:none!important;
}}
a[x-apple-data-detectors] {{
color:inherit!important;
text-decoration:none!important;
font-size:inherit!important;
font-family:inherit!important;
font-weight:inherit!important;
line-height:inherit!important;
}}
.es-desk-hidden {{
display:none;
float:left;
overflow:hidden;
width:0;
max-height:0;
line-height:0;
mso-hide:all;
}}
@media only screen and (max-width:600px) {{p, ul li, ol li, a {{ line-height:150%!important }} h1, h2, h3, h1 a, h2 a, h3 a {{ line-height:120% }} h1 {{ font-size:30px!important; text-align:left }} h2 {{ font-size:24px!important; text-align:left }} h3 {{ font-size:18px!important; text-align:left }} .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a {{ font-size:30px!important; text-align:left }} .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a {{ font-size:24px!important; text-align:left }} .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a {{ font-size:18px!important; text-align:left }} .es-menu td a {{ font-size:14px!important }} .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a {{ font-size:14px!important }} .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a {{ font-size:14px!important }} .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a {{ font-size:14px!important }} .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a {{ font-size:12px!important }} *[class=""gmail-fix""] {{ display:none!important }} .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 {{ text-align:center!important }} .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 {{ text-align:right!important }} .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 {{ text-align:left!important }} .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img {{ display:inline!important }} .es-button-border {{ display:inline-block!important }} a.es-button, button.es-button {{ font-size:16px!important; display:inline-block!important }} .es-adaptive table, .es-left, .es-right {{ width:100%!important }} .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header {{ width:100%!important; max-width:600px!important }} .es-adapt-td {{ display:block!important; width:100%!important }} .adapt-img {{ width:100%!important; height:auto!important }} .es-m-p0 {{ padding:0!important }} .es-m-p0r {{ padding-right:0!important }} .es-m-p0l {{ padding-left:0!important }} .es-m-p0t {{ padding-top:0!important }} .es-m-p0b {{ padding-bottom:0!important }} .es-m-p20b {{ padding-bottom:20px!important }} .es-mobile-hidden, .es-hidden {{ display:none!important }} tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden {{ width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important }} tr.es-desk-hidden {{ display:table-row!important }} table.es-desk-hidden {{ display:table!important }} td.es-desk-menu-hidden {{ display:table-cell!important }} .es-menu td {{ width:1%!important }} table.es-table-not-adapt, .esd-block-html table {{ width:auto!important }} table.es-social {{ display:inline-block!important }} table.es-social td {{ display:inline-block!important }} .es-desk-hidden {{ display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important }} .es-m-p5 {{ padding:5px!important }} .es-m-p5t {{ padding-top:5px!important }} .es-m-p5b {{ padding-bottom:5px!important }} .es-m-p5r {{ padding-right:5px!important }} .es-m-p5l {{ padding-left:5px!important }} .es-m-p10 {{ padding:10px!important }} .es-m-p10t {{ padding-top:10px!important }} .es-m-p10b {{ padding-bottom:10px!important }} .es-m-p10r {{ padding-right:10px!important }} .es-m-p10l {{ padding-left:10px!important }} .es-m-p15 {{ padding:15px!important }} .es-m-p15t {{ padding-top:15px!important }} .es-m-p15b {{ padding-bottom:15px!important }} .es-m-p15r {{ padding-right:15px!important }} .es-m-p15l {{ padding-left:15px!important }} .es-m-p20 {{ padding:20px!important }} .es-m-p20t {{ padding-top:20px!important }} .es-m-p20r {{ padding-right:20px!important }} .es-m-p20l {{ padding-left:20px!important }} .es-m-p25 {{ padding:25px!important }} .es-m-p25t {{ padding-top:25px!important }} .es-m-p25b {{ padding-bottom:25px!important }} .es-m-p25r {{ padding-right:25px!important }} .es-m-p25l {{ padding-left:25px!important }} .es-m-p30 {{ padding:30px!important }} .es-m-p30t {{ padding-top:30px!important }} .es-m-p30b {{ padding-bottom:30px!important }} .es-m-p30r {{ padding-right:30px!important }} .es-m-p30l {{ padding-left:30px!important }} .es-m-p35 {{ padding:35px!important }} .es-m-p35t {{ padding-top:35px!important }} .es-m-p35b {{ padding-bottom:35px!important }} .es-m-p35r {{ padding-right:35px!important }} .es-m-p35l {{ padding-left:35px!important }} .es-m-p40 {{ padding:40px!important }} .es-m-p40t {{ padding-top:40px!important }} .es-m-p40b {{ padding-bottom:40px!important }} .es-m-p40r {{ padding-right:40px!important }} .es-m-p40l {{ padding-left:40px!important }} }}
</style>
</head>
<body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
<div class=""es-wrapper-color"" style=""background-color:#042326""><!--[if gte mso 9]>
<v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
<v:fill type=""tile"" color=""#042326""></v:fill>
</v:background>
<![endif]-->
<table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#042326"">
<tr>
<td valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-header"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
<tr>
<td align=""center"" style=""padding:0;Margin:0"">
<table bgcolor=""#ffffff"" class=""es-header-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"">
<tr>
<td align=""left"" style=""Margin:0;padding-top:20px;padding-bottom:20px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;display:none""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table>
<table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
<tr>
<td align=""center"" style=""padding:0;Margin:0"">
<table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#0D0D0D;width:600px"">
<tr>
<td align=""left"" style=""padding:35px;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:15px;background-color:#042326;background-image:url(https://mlymuy.stripocdn.email/content/guids/CABINET_85e0d372587b5260d0fabc973c44f5e3f6d693f5d03f964ea4e13ac509775aa3/images/rectangle_5569_HbI.png);background-repeat:no-repeat;background-position:center center"" bgcolor=""#042326"" background=""https://mlymuy.stripocdn.email/content/guids/CABINET_85e0d372587b5260d0fabc973c44f5e3f6d693f5d03f964ea4e13ac509775aa3/images/rectangle_5569_HbI.png"" role=""presentation"">
<tr>
<td align=""center"" class=""es-m-txt-c"" style=""Margin:0;padding-bottom:20px;padding-left:20px;padding-right:20px;padding-top:35px""><h2 style=""Margin:0;line-height:34px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:28px;font-style:normal;font-weight:bold;color:#E4E4E4"">Thanks For You Order!</h2></td>
</tr>
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-bottom:20px;font-size:0px""><a target=""_blank"" href=""https://viewstripo.email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#E4E4E4;font-size:14px""><img src=""https://mlymuy.stripocdn.email/content/guids/CABINET_85e0d372587b5260d0fabc973c44f5e3f6d693f5d03f964ea4e13ac509775aa3/images/group.png"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" width=""65""></a></td>
</tr>
<tr>
<td align=""center"" class=""es-m-p20r es-m-p20l"" style=""Margin:0;padding-top:10px;padding-bottom:20px;padding-left:40px;padding-right:40px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Thank you for your recent order. We are pleased to confirm that we have received your order and it is currently being processed.</p></td>
</tr>
<tr>
<td align=""center"" style=""Margin:0;padding-top:10px;padding-left:20px;padding-right:20px;padding-bottom:35px""><!--[if mso]><a href=""https://viewstripo.email"" target=""_blank"" hidden>
<v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" esdevVmlButton href=""https://viewstripo.email""
style=""height:51px; v-text-anchor:middle; width:292px"" arcsize=""50%"" stroke=""f"" fillcolor=""#d98b8b"">
<w:anchorlock></w:anchorlock>
<center style='color:#042326; font-family:Sora, Arial, sans-serif; font-size:18px; font-weight:400; line-height:18px; mso-text-raise:1px'>Order Number: {i}</center>
</v:roundrect></a>
<![endif]--><!--[if !mso]><!-- --><span class=""msohide es-button-border"" style=""border-style:solid;border-color:#2CB543;background:#D98B8B;border-width:0px;display:inline-block;border-radius:30px;width:auto;mso-border-alt:10px;mso-hide:all""><a href=""https://viewstripo.email"" class=""es-button"" target=""_blank"" style=""mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#042326;font-size:18px;display:inline-block;background:#D98B8B;border-radius:30px;font-family:Sora, Arial, sans-serif;font-weight:normal;font-style:normal;line-height:22px;width:auto;text-align:center;padding:15px 20px 15px 20px"">Order Number: {i}</a></span><!--<![endif]--></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table>
<table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
<tr>
<td align=""center"" style=""padding:0;Margin:0"">
<table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#0D0D0D;width:600px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" class=""es-m-txt-c"" style=""padding:0;Margin:0""><h2 style=""Margin:0;line-height:34px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:28px;font-style:normal;font-weight:bold;color:#E4E4E4"">Order Summary</h2></td>
</tr>
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-bottom:10px;padding-top:20px;font-size:0"">
<table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td style=""padding:0;Margin:0;border-bottom:1px solid #d98b8b;background:unset;height:1px;width:100%;margin:0px""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td class=""esdev-adapt-off"" align=""left"" style=""padding:0;Margin:0;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" class=""esdev-mso-table"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:530px"">
<tr>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td class=""es-m-p0r"" align=""center"" style=""padding:0;Margin:0;width:164px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Item</p></td>
</tr>
</table></td>
</tr>
</table></td>
<td style=""padding:0;Margin:0;width:20px""></td>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;width:163px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Information</p></td>
</tr>
</table></td>
</tr>
</table></td>
<td style=""padding:0;Margin:0;width:20px""></td>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;width:163px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Price</p></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:20px;font-size:0"">
<table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td style=""padding:0;Margin:0;border-bottom:1px solid #d98b8b;background:unset;height:1px;width:100%;margin:0px""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>";
            User user = JsonSerializer.Deserialize<User>(File.ReadAllText("current_user.json"));
            foreach (Product product in Products)
            {
                mail.Body += $@"
<tr>
<td class=""esdev-adapt-off"" align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" class=""esdev-mso-table"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:530px"">
<tr>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td class=""es-m-p0r"" align=""center"" style=""padding:0;Margin:0;width:164px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-top:5px;font-size:0px""><a target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#E4E4E4;font-size:14px""><img src=""{product.Mouse.ImageLink}"" alt style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"" height=""150"" class=""adapt-img p_image""></a></td>
</tr>
</table></td>
</tr>
</table></td>
<td style=""padding:0;Margin:0;width:20px""></td>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;width:163px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0""><h3 class=""p_name"" style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">{product.Mouse.Company}<br>{product.Mouse.Model}</h3><br>Quantity: {product.Count}</p></td>
</tr>
</table></td>
</tr>
</table></td>
<td style=""padding:0;Margin:0;width:20px""></td>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;width:163px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0""><p class=""p_price"" style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">${product.Mouse.Price}</p></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>";
            }
mail.Body+= $@"
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" class=""es-m-txt-c"" style=""padding:0;Margin:0""><h2 style=""Margin:0;line-height:34px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:28px;font-style:normal;font-weight:bold;color:#E4E4E4"">Payment Summary</h2></td>
</tr>
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-bottom:10px;padding-top:20px;font-size:0"">
<table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td style=""padding:0;Margin:0;border-bottom:1px solid #d98b8b;background:unset;height:1px;width:100%;margin:0px""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td class=""esdev-adapt-off"" align=""left"" style=""Margin:0;padding-top:10px;padding-bottom:10px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" class=""esdev-mso-table"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:530px"">
<tr>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:347px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Subtotal</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Shipping</p></td>
</tr>
</table></td>
</tr>
</table></td>
<td style=""padding:0;Margin:0;width:20px""></td>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:163px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:15px;background-color:#042326;background-image:url(https://mlymuy.stripocdn.email/content/guids/CABINET_b4350b9331e5ec2b224eae0851e3eaf0e29a7d940812677b1e58d30c73b776b4/images/rectangle_5569.png);background-repeat:no-repeat;background-position:center center"" bgcolor=""#042326"" background=""https://mlymuy.stripocdn.email/content/guids/CABINET_b4350b9331e5ec2b224eae0851e3eaf0e29a7d940812677b1e58d30c73b776b4/images/rectangle_5569.png"" role=""presentation"">
<tr>
<td align=""center"" style=""padding:10px;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">${subtotal}<br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Free</p></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px;font-size:0"">
<table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td style=""padding:0;Margin:0;border-bottom:1px solid #d98b8b;background:unset;height:1px;width:100%;margin:0px""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td class=""esdev-adapt-off"" align=""left"" style=""Margin:0;padding-top:10px;padding-left:35px;padding-right:35px;padding-bottom:40px"">
<table cellpadding=""0"" cellspacing=""0"" class=""esdev-mso-table"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:530px"">
<tr>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:347px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">Total</h3></td>
</tr>
</table></td>
</tr>
</table></td>
<td style=""padding:0;Margin:0;width:20px""></td>
<td class=""esdev-mso-td"" valign=""top"" style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:163px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:15px;background-color:#042326;background-image:url(https://mlymuy.stripocdn.email/content/guids/CABINET_b4350b9331e5ec2b224eae0851e3eaf0e29a7d940812677b1e58d30c73b776b4/images/rectangle_5569.png);background-repeat:no-repeat;background-position:center center"" bgcolor=""#042326"" background=""https://mlymuy.stripocdn.email/content/guids/CABINET_b4350b9331e5ec2b224eae0851e3eaf0e29a7d940812677b1e58d30c73b776b4/images/rectangle_5569.png"" role=""presentation"">
<tr>
<td align=""center"" style=""padding:10px;Margin:0""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">${subtotal}</h3></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" class=""es-m-txt-c"" style=""padding:0;Margin:0""><h2 style=""Margin:0;line-height:34px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:28px;font-style:normal;font-weight:bold;color:#E4E4E4"">Your Details</h2></td>
</tr>
<tr>
<td align=""center"" style=""padding:0;Margin:0;padding-bottom:10px;padding-top:20px;font-size:0"">
<table border=""0"" width=""100%"" height=""100%"" cellpadding=""0"" cellspacing=""0"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td style=""padding:0;Margin:0;border-bottom:1px solid #d98b8b;background:unset;height:1px;width:100%;margin:0px""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
<tr>
<td align=""left"" style=""Margin:0;padding-top:10px;padding-left:35px;padding-right:35px;padding-bottom:40px""><!--[if mso]><table style=""width:530px"" cellpadding=""0"" cellspacing=""0""><tr><td style=""width:255px"" valign=""top""><![endif]-->
<table cellpadding=""0"" cellspacing=""0"" align=""left"" class=""es-left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td class=""es-m-p20b"" align=""left"" style=""padding:0;Margin:0;width:255px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:10px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Name</p></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:15px""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">{user.Name} {user.Surname}</h3></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:15px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Email</p></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:10px""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">{user.Mail}</h3></td>
</tr>
</table></td>
</tr>
</table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:255px"" valign=""top""><![endif]-->
<table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:255px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:15px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">Order date</p></td>
</tr>
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-top:5px;padding-bottom:10px""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">{DateTime.Now}</h3></td>
</tr>
</table></td>
</tr>
</table><!--[if mso]></td></tr></table><![endif]--></td>
</tr>
</table></td>
</tr>
</table>
<table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
<tr>
<td align=""center"" style=""padding:0;Margin:0"">
<table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#0D0D0D;width:600px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;padding-left:35px;padding-right:35px;padding-top:40px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-left:1px solid #D98B8B;border-right:1px solid #D98B8B;border-top:1px solid #D98B8B;border-bottom:1px solid #D98B8B;border-radius:100px"" role=""presentation"">
<tr>
<td align=""center"" height=""30"" style=""padding:0;Margin:0""></td>
</tr>
<tr>
<td align=""center"" style=""Margin:0;padding-bottom:10px;padding-top:20px;padding-left:20px;padding-right:20px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#E4E4E4;font-size:14px"">need a help?</p></td>
</tr>
<tr>
<td align=""center"" class=""es-m-txt-c es-m-p0t"" style=""padding:20px;Margin:0""><h2 style=""Margin:0;line-height:34px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:28px;font-style:normal;font-weight:bold;color:#E4E4E4"">Customer care support</h2></td>
</tr>
<tr>
<td style=""padding:0;Margin:0"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""es-menu"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr class=""links-images-top"">
<td align=""center"" valign=""top"" width=""100%"" style=""Margin:0;padding-top:10px;padding-bottom:20px;padding-left:10px;padding-right:10px;border:0"" id=""esd-menu-id-0""><a target=""_blank"" href=""https://viewstripo.email"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:Sora, Arial, sans-serif;color:#E4E4E4;font-size:14px""><img src=""https://mlymuy.stripocdn.email/content/guids/CABINET_85e0d372587b5260d0fabc973c44f5e3f6d693f5d03f964ea4e13ac509775aa3/images/icons8mail48.png"" alt=""rasimbabayev9g19@gmail.com"" title=""rasimbabayev9g19@gmail.com"" align=""absmiddle"" width=""20"" style=""display:inline-block !important;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;padding-bottom:5px;padding-right:15px;vertical-align:middle""><br>rasimbabayev9g19@gmail.com</a></td>
</tr>
</table></td>
</tr>
<tr>
<td align=""center"" height=""30"" style=""padding:0;Margin:0""></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table>
<table cellpadding=""0"" cellspacing=""0"" class=""es-footer"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
<tr>
<td align=""center"" style=""padding:0;Margin:0"">
<table bgcolor=""#ffffff"" class=""es-footer-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#0D0D0D;width:600px"">
<tr>
<td align=""left"" style=""Margin:0;padding-left:35px;padding-right:35px;padding-top:40px;padding-bottom:40px""><!--[if mso]><table style=""width:530px"" cellpadding=""0""
cellspacing=""0""><tr><td style=""width:293px"" valign=""top""><![endif]-->
<table cellpadding=""0"" cellspacing=""0"" class=""es-left"" align=""left"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left"">
<tr>
<td class=""es-m-p20b"" align=""left"" style=""padding:0;Margin:0;width:293px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" class=""es-m-txt-l"" style=""padding:0;Margin:0""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">OPENING HOURS</h3><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#999999;font-size:14px""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#999999;font-size:14px"">Mon - Fri: 8AM - 10PM<br>Sat: 9AM-8PM<br>Sun: Closed<br>National Holidays: Closed</p></td>
</tr>
</table></td>
</tr>
</table><!--[if mso]></td><td style=""width:20px""></td><td style=""width:217px"" valign=""top""><![endif]-->
<table cellpadding=""0"" cellspacing=""0"" class=""es-right"" align=""right"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right"">
<tr>
<td align=""left"" style=""padding:0;Margin:0;width:217px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""left"" style=""padding:0;Margin:0""><h3 style=""Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;font-size:20px;font-style:normal;font-weight:bold;color:#E4E4E4"">CONTACT US</h3><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:21px;color:#999999;font-size:14px""><a target=""_blank"" href=""tel:+994773272763"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#999999;font-size:14px"">+994773272763<br>&nbsp;</a><a target=""_blank"" href=""mailto:rasimbabayev9g19@gmail.com"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#999999;font-size:14px"">rasimbabayev9g19@gmail.com</a></p></td>
</tr>
</table></td>
</tr>
</table><!--[if mso]></td></tr></table><![endif]--></td>
</tr>
</table></td>
</tr>
</table>
<table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
<tr>
<td class=""es-info-area"" align=""center"" style=""padding:0;Margin:0"">
<table class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"">
<tr>
<td align=""left"" style=""Margin:0;padding-top:20px;padding-bottom:20px;padding-left:35px;padding-right:35px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:530px"">
<table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
<tr>
<td align=""center"" class=""es-infoblock"" style=""padding:0;Margin:0;padding-top:15px;padding-bottom:30px;line-height:14px;font-size:12px;color:#CCCCCC""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Sora, Arial, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px"">No longer want to receive these emails? <a target=""_blank"" href="""" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px"">Unsubscribe.</a><br>6733 Golden Circle, Possession, Nevada</p></td>
</tr>
<tr>
<td align=""center"" class=""es-infoblock made_with"" style=""padding:0;Margin:0;line-height:120%;font-size:0;color:#CCCCCC""><a target=""_blank"" href=""https://viewstripo.email/?utm_source=templates&utm_medium=email&utm_campaign=gadget_18&utm_content=gadget_order_confirmed"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px""><img src=""https://mlymuy.stripocdn.email/content/guids/CABINET_09023af45624943febfa123c229a060b/images/7911561025989373.png"" alt width=""125"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table></td>
</tr>
</table>
</div>
</body>
</html>";
            SendReciept(mail);
        }
        public void SendReciept(MailMessage mail)
        {
            SmtpClient SmtpServer = new("smtp.gmail.com");
            mail.From = new MailAddress("rasimbabayev9g19@gmail.com");
            User user = JsonSerializer.Deserialize<User>(File.ReadAllText("current_user.json"));
            mail.To.Add(user.Mail);
            mail.Subject = "Your reciept";
            mail.IsBodyHtml = true;

            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new System.Net.NetworkCredential("rasimbabayev9g19@gmail.com", "gitobwidmpsvwsdv");
            SmtpServer.EnableSsl = true;
            SmtpServer.SendMailAsync(mail);
            MessageBox.Show("We've sent you your reciept!");
        }
    }
}
