<%@ Page Title="Home" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TheFoodCompany.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="Home.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <nav>
            <h2>Categories</h2>
            <hr />
            <asp:Repeater ID="rptCategories" runat="server">
                <ItemTemplate>
                    <a href='/Home.aspx?category=<%#Eval("categoryId") %>'><%#Eval("name") %></a>
                    <br />
                </ItemTemplate>
            </asp:Repeater>
        </nav>

        <section>
            <a href="ProductDetails.aspx" runat="server">
                <h2>Products</h2>
            </a>
            <hr />
            <asp:Repeater ID="rptProducts" runat="server">
                <HeaderTemplate>
                    <h3><%#Eval("name") %></h3>
                    <hr />
                </HeaderTemplate>

                <ItemTemplate>
                        <img src='<%#Eval("product_image") %>' alt='<%#Eval("altText") %>' />

                        <br />

                        <p>
                            Price: <%#Eval("price") %>
                            <br />
                            <%#Eval("briefDescription") %>
                        </p>

                        <br />

                        <input type="button" value="Add to Cart" onclick='viewProduct(<%#Eval("productId")%>)' /><!--Need to setup onclicks-->
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </section>
    </div>
</asp:Content>
