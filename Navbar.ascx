<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Navbar.ascx.cs" Inherits="DogeBook.Navbar" %>

<nav class="navbar navbar-expand-lg navbar-light bg-light">
  <a class="navbar-brand" href="#">DogeBook</a>
  <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
    <span class="navbar-toggler-icon"></span>
  </button>
  <div class="collapse navbar-collapse" id="navbarNav">
    <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link" href="#">Timeline</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="Profile.aspx">Profile</a>
      </li>
      <li class="nav-item">
        <a class="nav-link" href="#">Friends</a>
      </li>
    </ul>
  </div>
</nav>