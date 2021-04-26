<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="DogeBook.Navbar" %>

<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="Timeline.aspx">DogeBook <i class="fas fa-paw"></i></a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarNav">
    <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link" href="Timeline.aspx">Timeline</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="Profile.aspx">Profile</a>
      </li>

      <li class="nav-item">
        <a class="nav-link" href="FriendRequests.aspx">Friend Requests</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="BrowseFriends.aspx">Browse Friends</a>
      </li>
      <li class="nav-item" >
        <a id="BtnLogOut" class="nav-link text-danger" href="Login.aspx">Log Out</a>
      </li>
    </ul>
  </div>
</nav>