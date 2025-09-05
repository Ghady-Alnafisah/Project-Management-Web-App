<%@ Page Title="إضافة مهام" Language="C#" MasterPageFile="~/LoggedUsers.Master" AutoEventWireup="true" CodeBehind="CreateTask.aspx.cs" Inherits="TasksTrackerWebApp.CreateTask" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


     <script src="Scripts/taskSelection.js"></script>

    <h2>إنشاء مهمة جديدة</h2>
    <div class="form-group">
        <asp:HiddenField ID="hdnTaskId" runat="server" Value="0" />        <asp:Label runat="server" Text="اسم المهمة*" AssociatedControlID="txtTitle" />
        <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" required="true" />
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="*الوصف" AssociatedControlID="txtDescription" />
        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" required="true" />
      
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="تاريخ الاستحقاق*" AssociatedControlID="txtDueDate" />
        <asp:TextBox ID="txtDueDate" runat="server" CssClass="form-control" TextMode="Date" required="true" />
          <asp:CompareValidator runat="server" ControlToValidate="txtDueDate"

         ErrorMessage="يرجى إدخال التاريخ بصيغة صحيحة (YYYY-MM-DD)"
          Operator="DataTypeCheck" Type="Date" 
            Display="Dynamic" CssClass="text-danger" />
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="الأولوية*" AssociatedControlID="ddlPriority" />
        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control" required="true">
            <asp:ListItem Value="Low" Text="منخفضة" />
            <asp:ListItem Value="Medium" Text="متوسطة" />
            <asp:ListItem Value="High" Text="عالية" />
         </asp:DropDownList>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="حالة الإنجاز*" AssociatedControlID="ddlStatus" />
        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" required="true">
            <asp:ListItem Value="New" Text="جديدة" />
            <asp:ListItem Value="Archived" Text="مؤرشفة" />
            <asp:ListItem Value="In Progress" Text="قيد العمل" />
            <asp:ListItem Value="Completed" Text="مكتملة" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <asp:Label runat="server" Text="التصنيف*" AssociatedControlID="ddlCategory" />
        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" DataTextField="CategoryName" DataValueField="CategoryId" required="true" />

        <asp:Label runat="server" Text="إسناد إلى*" AssociatedControlID="ddlUsers" />
        <asp:DropDownList ID="ddlUsers" runat="server" CssClass="form-control" DataTextField="UserName" DataValueField="UserId" required="true" />


        <asp:Button ID="btnCreateTask" runat="server" Text="حفظ" 
        CssClass="btn btn-primary" OnClick="btnCreateTask_Click" />


        <asp:Button ID="btnUpdateTask" runat="server" Text="حفظ التغييرات" CssClass="btn btn-warning" OnClick="btnUpdateTask_Click" Visible="false" />
        <asp:Button ID="btnCancelUpdate" runat="server" Text="إلغاء" CssClass="btn btn-secondary" OnClick="btnCancelUpdate_Click" Visible="false" />


        <asp:Button 
            ID="btnTest" 
            runat="server" 
            Text="إرسال عبر البريد الإلكتروني" 
            OnClick="btnTest_Click" 
        />
    </div>


    <div class="row mb-3">
        <div class="col-md-6">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="بحث..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged">
            </asp:TextBox>
        </div>
    </div>


    <div class ="mt-5">
        <h3>قائمة المهام</h3>
        <asp:Repeater ID="rptTasks" runat="server" OnItemCommand="rptTasks_ItemCommand">
            <HeaderTemplate>
                <table class="table table-striped table-bordered">
                    <thead class="thead-dark">
                        <tr>
                            <th>العنوان</th>
<th>الوصف</th>
<th>تاريخ الاستحقاق</th>
<th>الأولوية</th>
<th>الحالة</th>
<th>التصنيف</th>
<th>مسند إلى</th>
<th>الإجراءات</th>

                        </tr>
                    </thead>
                    <tbody>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <tr id="taskRow" runat="server">
                        <td><%# Eval("Title") %></td>
                        <td><%# Eval("Description") %></td>
                        <td><%# Eval("DueDate", "{0:MM/dd/yyyy}") %></td>
                        <td><%# Eval("Priority") %></td>
                        <td><%# Eval("Status") %></td>
                        <td><%# Eval("CategoryName") %></td>
                        <td><%# Eval("UserName") %></td>
                        <td>
                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("TaskId") %>' CssClass="btn btn-sm btn-info">
                            <i class="fas fa-edit"></i>تحرير
                        </asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("TaskId") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('هل أنت متأكد أنك تريد حذف هذه المهمة؟');">
                            <i class="fas fa-trash"></i>حذف
                        </asp:LinkButton> 
                        <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CommandArgument='<%# Eval("TaskId") %>'  CssClass="btn btn-sm btn-info" CausesValidation="false">
                            <i class="fas fa-check"></i>اختيار
                        </asp:LinkButton> 
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </tbody>
               </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>


<asp:Label ID="lblStatus" runat="server" CssClass="status-message" Visible="false" />


</asp:Content>


