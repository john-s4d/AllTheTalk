<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default_old.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="CustomControls" Namespace="AllTheTalk" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml"
    xmlns:o="urn:schemas-microsoft-com:office:office">
<head runat="server">
    <meta http-equiv="Content-Language" content="en-us" />
    <title>AllTheTalk.com | The only source you need</title>
    <link rel="stylesheet" type="text/css" href="styles/allthetalk.css" />
    <style type="text/css">
.style2 {
	padding-top: 10px;
	text-align: right;
}
.style3 {
	padding-left: 20px;
	padding-right: 20px;
	padding-top: 10px;
	text-align: right;
	font-family: verdana, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	width: auto;
}
.style4 {
	padding-left: 20px;
	padding-right: 20px;
	padding-top: 10px;
	text-align: left;
	font-family: verdana, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	vertical-align: top;
}
.style6 {
	padding-left: 20px;
	padding-right: 20px;
	padding-top: 10px;
	text-align: center;
	font-family: verdana, Arial, Helvetica, sans-serif;
	font-size: 8pt;
}
.style7 {
	padding-left: 0;
	padding-right: 10px;
	padding-top: 10px;
	text-align: right;
	font-family: verdana, Arial, Helvetica, sans-serif;
	font-size: 8pt;
	vertical-align: top;
}
.style8 {
	padding: 3px;
	text-align: center;
	vertical-align: middle;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
        <table id="pagetable" class="pageholder">
            <tr>
                <td colspan="4">
                    <table class="header" id="table1">
                        <tr>
                            <td class="header-logo">
                                <div class="logo-main">
                                    AllTheTalk.com</div>
                                <div class="logo-tag">
                                    The only source you need</div>
                            </td>
                            <td class="style7">
                                Hello, <strong>John Sessford<br />
                                </strong>You are an <strong>Administrator<br />
                                </strong>You have <strong>1 </strong>Unread Message</td>
                            <td class="header-ad">
                                <cc1:Advertisement ID="Advertisement3" runat="server" Size="HalfBanner_234x60" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="left-column" rowspan="4">
                    <table id="table3">
                        <tr>
                            <td class="column-side">
                                <asp:DataList ID="dlCategory" runat="server" RepeatLayout="Flow" ShowFooter="False"
                                    ShowHeader="True">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlCategory" runat="server" NavigateUrl='<%# Eval("Name", "Default.aspx?category={0}") %>'
                                            Text='<%# Eval("Name") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <SelectedItemTemplate>
                                        <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Name") %>' CssClass="category-selected">
                                        </asp:Label>
                                    </SelectedItemTemplate>
                                </asp:DataList>
                            </td>
                        </tr>
                        <tr>
                            <td class="column-side">
                                Search:<br />
                                <input name="txtSearch" size="12" class="input" />
                                <asp:Button runat="server" Text="GO" ID="btnSearch" Height="14pt" Font-Size="8pt"
                                    CausesValidation="False" Width="18pt" />
                                <br />
                                <a href="#">Advanced</a></td>
                        </tr>
                        <tr>
                            <td class="column-side">
                                <cc1:Advertisement ID="Advertisement1" runat="server" Size="Skyscraper_120x600" />
                            </td>
                        </tr>
                        <tr>
                            <td class="column-side">
                                <strong>Add Content<br />
                                    Add RSS Feed<br />
                                    Site Feedback</strong></td>
                        </tr>
                        <tr>
                            <td class="column-side">
                                Username:<br />
                                <input name="txtSearch0" size="12" class="input" /><br />
                                Password:<br />
                                <input name="txtSearch1" size="12" class="input" /><br />
                                Login now<br />
                                <br />
                                New Account</td>
                        </tr>
                        <tr>
                            <td class="column-side">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <cc1:Advertisement ID="Advertisement6" runat="server" />
                </td>
                <td class="content" colspan="2">
                    <table style="float: right;">
                        <tr>
                            <td class="style8">
                                <a href="http://del.icio.us/post" onclick="window.open('http://del.icio.us/post?v=4&noui&jump=close&url='+encodeURIComponent(location.href)+'&title='+encodeURIComponent(document.title), 'delicious','toolbar=no,width=700,height=400'); return false;">
                                    <img alt="del.icio.us" src="images/ico_delicious.gif" width="16" height="16" /></a></td>
                            <td class="style8">
                                <img alt="reddit" src="images/ico_reddit.gif" /></td>
                            <td class="style8">
                                <img alt="digg" src="images/ico_digg.gif" /></td>
                        </tr>
                        <tr>
                            <td class="style8">
                                <a href="http://del.icio.us/post" onclick="window.open('http://del.icio.us/post?v=4&noui&jump=close&url='+encodeURIComponent(location.href)+'&title='+encodeURIComponent(document.title), 'delicious','toolbar=no,width=700,height=400'); return false;">
                                </a>
                                <img alt="Furl" src="images/ico_furl.gif" /></td>
                            <td class="style8">
                                <img alt="spurl" src="images/ico_spurl.jpg" /></td>
                            <td class="style8">
                                <img alt="StumbleUpon" src="images/ico_stumbleupon.png" width="17" height="16" /></td>
                        </tr>
                        <tr>
                            <td class="style8">
                                <img alt="Print" src="images/ico_print.gif" width="16" height="16" /></td>
                            <td class="style8">
                                <img alt="Email" src="images/ico_email.gif" width="16" height="16" /></td>
                            <td class="style8">
                                <img height="16" alt="" src="images/ico_rss.png" width="16" /></td>
                        </tr>
                    </table>
                    <span class="content-title">Title Goes Here</span><br />
                    <span class="content-summary">Short Summary<br />
                        <br />
                    </span>Permalink | Channel | Author | Date Added</td>
                <td class="right-column" rowspan="4">
                    <table id="table2">
                        <tr>
                            <td class="column-side">
                                <cc1:Advertisement ID="Advertisement2" runat="server" Size="Skyscraper_120x600" />
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                Visitors Online:<br />
                                Visitors Today:<br />
                                New Articles:<br />
                                Total Articles:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="content" colspan="2">
                    <cc1:Advertisement ID="Advertisement4" runat="server" CssClass="ad-content-left"
                        Size="SmallSquare_200x200" />
                    Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Fusce augue. Phasellus
                    justo risus, lobortis sit amet, fringilla quis, sodales congue, ante. Aliquam erat
                    volutpat. Nulla suscipit vestibulum sapien. Sed quam lectus, convallis vitae, semper
                    sed, rutrum non, est. Nullam mauris nulla, pharetra in, tincidunt vel, vulputate
                    et, elit. Integer ut felis t lacus ornare eleifend. Nullam varius. Nam rhoncus,
                    nisl rutrum scelerisque sagittis, nulla nibh molestie erat, id hendrerit turpis
                    mi vitae lacus. Vestibulum tincidunt dignissim ipsum. Curabitur ante nulla, pharetra
                    nec, dignissim et, ornare sit amet, nisi.<br />
                    <br />
                    Fusce nec neque vitae nulla lobortis ullamcorper. Phasellus purus tellus, sollicitudin
                    a, tristique ut, ultrices et, quam. Ut feugiat risus non quam. Suspendisse potenti.
                    Quisque a erat. Nunc eleifend. Fusce ac augue vitae mi ullamcorper facilisis. Nullam
                    in erat. Etiam quis enim. In hac habitasse platea dictumst. Vestibulum ac lorem
                    et mauris iaculis fringilla. Integer viverra. Aliquam nonummy. Cras augue nisl,
                    feugiat non, bibendum in, fringilla non, odio. Donec suscipit quam. Pellentesque
                    habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.
                    Suspendisse sem mauris, porttitor id, interdum sed, consequat et, nulla.<br />
                    <br />
                    Cras vestibulum laoreet diam. Donec vel eros.Vivamus molestie semper tortor. Nam
                    auctor. Phasellus arcu nunc, tristique quis, volutpat quis, aliquam a, magna. Suspendisse
                    nec tellus et elit sagittis bibendum. Etiam interdum pede faucibus eros. Nam turpis
                    ante, posuere eu, vulputate eget, fermentum in, odio. Pellentesque ullamcorper,
                    neque a ullamcorper pellentesque, nunc mi porttitor mauris, at faucibus sem orci
                    et massa. Donec tincidunt ultrices nunc. Sed sed eros. Sed sapien elit, semper vitae,
                    sodales at, faucibus faucibus, lorem. Nunc suscipit ligula lobortis urna. Mauris
                    cursus auctor nulla. Mauris porta semper diam. Class aptent taciti sociosqu ad litora
                    torquent per conubia nostra, per inceptos hymenaeos. Aenean interdum tincidunt nisi.
                    Morbi tristique. Praesent est. Curabitur est.<br />
                    <br />
                    Etiam elit metus, volutpat nec, bibendum a, aliquam non, ipsum. Aliquam tristique,
                    purus quis sodales pulvinar, dolor justo semper lectus, quis nonummy augue nisi
                    volutpat metus. Curabitur dapibus nisi ut ipsum. Cras nec odio vitae nulla sagittis
                    aliquam. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur
                    ridiculus mus. Donec ac odio. Nullam nisl tellus, tristique eu, rhoncus vitae, placerat
                    in, leo. In hac habitasse platea dictumst. Mauris non nisi vel neque rhoncus tempor.
                    Aliquam erat volutpat. Nullam sollicitudin. Praesent elit est, nonummy sed, vulputate
                    commodo, accumsan at, nisi. Maecenas justo nibh, semper ac, facilisis at, vehicula
                    ut, urna. Curabitur sed ipsum id est adipiscing placerat. Nunc pede. Nam nibh metus,
                    sollicitudin id, suscipit sed, blandit in, nulla. Curabitur nec eros eget nibh interdum
                    pulvinar. Suspendisse vitae odio id nisi blandit sodales. Phasellus at neque at
                    augue tempus suscipit.<br />
                    <br />
                    Donec scelerisque sem. Sed tincidunt turpis non sapien. Suspendisse purus. Etiam
                    adipiscing congue ante. Aenean scelerisque pede posuere elit. Donec nec enim. Etiam
                    aliquam purus in orci. Vivamus est. Vestibulum venenatis, ipsum eget sodales eleifend,
                    augue erat porta massa, id lacinia sapien eros et nulla. Aliquam tristique arcu
                    ac est. Nullam ullamcorper. Praesent sit amet lectus. Sed mattis pretium felis.
                    Proin luctus adipiscing nunc. Morbi gravida interdum nulla. Praesent nec est. Donec
                    adipiscing felis et nisl. Aliquam volutpat erat eu elit rhoncus tempus.<br />
                </td>
            </tr>
            <tr>
                <td class="style4">
                    <strong>Rate This Article: 1 | 2 | 3 | 4 | 5</strong></td>
                <td class="style3">
                    <strong>&lt;&lt; Prev | 1 | 2 | 3 | 4 | 5 | Next &gt;&gt;</strong></td>
            </tr>
            <tr>
                <td class="content" colspan="2">
                    <span class="ad-content-lower">
                        <cc1:Advertisement ID="Advertisement5" runat="server" Size="SmallSquare_200x200" />
                    </span>
                    <asp:DataList ID="dlCategory0" runat="server" RepeatLayout="Flow" ShowFooter="False"
                        ShowHeader="True">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTitle" runat="server" NavigateUrl='<%# Eval("Name", "Default.aspx?category={0}") %>'
                                Text='<%# Eval("Name") %>'>
                            </asp:HyperLink>
                            <asp:Label runat="server" ID="lblShortSummary" Text='<%# Eval("shortSummary") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <strong>Related Articles:</strong>
                        </HeaderTemplate>
                        <SelectedItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Name") %>' CssClass="category-selected">
                            </asp:Label>
                        </SelectedItemTemplate>
                    </asp:DataList><br />
                    <br />
                    <asp:DataList ID="dlCategory1" runat="server" RepeatLayout="Flow" ShowFooter="False"
                        ShowHeader="True">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlTitle" runat="server" NavigateUrl='<%# Eval("Name", "Default.aspx?category={0}") %>'
                                Text='<%# Eval("Name") %>'>
                            </asp:HyperLink>
                            <asp:Label runat="server" ID="lblShortSummary" Text='<%# Eval("shortSummary") %>'>
                            </asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <strong>Popular Articles:</strong>
                        </HeaderTemplate>
                        <SelectedItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Name") %>' CssClass="category-selected">
                            </asp:Label>
                        </SelectedItemTemplate>
                    </asp:DataList></td>
            </tr>
            <tr>
                <td class="style6" colspan="4">
                    FAQ | Contact Us | Privacy | Terms of Use | Job Opportunities | Advertise</td>
            </tr>
            <tr>
                <td class="style6" colspan="4">
                    Copyright©2007 AllTheTalk.com</td>
            </tr>
            <tr>
                <td colspan="4" class="style6">
                    <asp:Label ID="lblResponse" runat="server" ForeColor="Green" EnableTheming="True">
                    </asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
