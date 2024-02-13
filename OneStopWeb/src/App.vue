<script setup>
import { RouterLink, RouterView } from 'vue-router'
</script>

<template>
  <header>
    <div class="nav-link">
      <img alt="Vue logo" class="logo" src="@/assets/logo.svg" width="56" height="56" />

      <div class="wrapper">
        <nav>
          <RouterLink to="/">Home</RouterLink>
          <RouterLink to="/about">placeholder</RouterLink>
          <RouterLink to="/about">placeholder</RouterLink>
          <RouterLink to="/about">placeholder</RouterLink>
        </nav>
      </div>
    </div>

    <div class="user-profile">
      <RouterLink to="/login">Login</RouterLink>
    </div>
  </header>

  <div class="main-content">

    <RouterView />
  </div>
</template>

<style lang="scss" scoped>
@import "@/assets/styles/app.scss";

* {
  // border: 1px solid white;
}

header {
  max-height: 100vh;
  widows: 100dvw !important;
  position: fixed;
  inset: 0 0;
  height: 64px;

  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;

  .logo {
    margin: auto 1rem;
  }

  .nav-link {
    display: flex;
    flex-direction: row;
    justify-content: center;
    align-items: center;
  }

  .user-profile {
    margin: auto 2rem;
  }

  nav {
    // width: 100%;
    margin: auto;
    font-size: 12px;
    text-align: center;

    display: flex;
    flex-direction: row;
  }

  nav a.router-link-exact-active {
    color: var(--color-text);
  }

  nav a.router-link-exact-active:hover {
    background-color: transparent;
  }

  nav a {
    display: inline-block;
    padding: 0 1rem;
    border-left: 1px solid var(--color-border);
  }

}

.main-content {
  width: 100dvw;
  height: calc(100dvh - 64px);
  position: fixed;
  bottom: 0;
  left: 0;
}
</style>
