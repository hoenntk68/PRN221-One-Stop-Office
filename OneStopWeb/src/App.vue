<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { useAuthStore } from '@/stores/auth';

const { state, authLogout } = useAuthStore();

import Cookies from 'js-cookie';

if (Cookies.get('token')) {
  state.isLoggedin = true;
  state.username = Cookies.get('fullname');
  state.isAdmin = Cookies.get('isAdmin') == 'true';
  state.isSuperAdmin = Cookies.get('isSuperAdmin') == 'true';
  console.log('state', state);
} else {
  state.isLoggedin = false;
  state.username = '';
  state.isAdmin = false;
  state.isSuperAdmin = false;
}
</script>

<template>
  <div>
    <header>
      <div class="nav-link">
        <img alt="logo" class="logo" src="@/assets/quoc-huy2.png" width="48" height="48" />

        <div class="wrapper">
          <nav>
            <RouterLink to="/">Home</RouterLink>
            <!-- <RouterLink to="/about">placeholder</RouterLink> -->
          </nav>
        </div>
      </div>

      <nav class="sidebar" v-if="state.isLoggedin">
        <ul>
          <li>
            <router-link to="/request-submit">
              Nop don
            </router-link>
          </li>
          <li>
            <router-link to="/request-list">
              Tra cuu
            </router-link>
          </li>
          <li v-if="state.isAdmin">
            <router-link to="/request-list">
              Users
            </router-link>
          </li>
          <li v-if="state.isSuperAdmin">
            <router-link to="/statistics">
              Statistic
            </router-link>
          </li>
          <li>
            <router-link to="/settings">
              Settings
            </router-link>
          </li>
        </ul>
      </nav>

      <div class="user-profile">
        <RouterLink v-if="!state.isLoggedin" to="/login">Login</RouterLink>
        <div v-else>
          {{ state.username }}
          <RouterLink @click="authLogout" to="/login">Logout</RouterLink>
        </div>
      </div>

    </header>

    <div class="main-content">
      <RouterView />
    </div>

    <footer>
      <p>Mọi góp ý, thắc mắc xin liên hệ: Văn phòng dịch vụ chính phủ: Email:
        <a href="mailto:dichvusinhvien@fe.edu.vn">vanphongchinhphu@gov.vn</a>.
        Điện thoại: <a href="tel:02473081313">024.7308.13.13</a> hoặc
        <a href="tel:02473081313">024.7308.13.13</a>
        © Powered by FPTU | ANTT | HOENTK | 24x7
      </p>
    </footer>


  </div>
</template>

<style lang="scss" scoped>
@import "@/assets/styles/app.scss";

* {
  // border: 1px solid black !important;
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

  .sidebar {
    position: fixed;
    left: 0;
    top: 64px;
    z-index: 10000 !important;
    width: 200px;
    height: 100dvh;

    ul {
      list-style: none;
      text-decoration: none;
      padding: 0px;

      li {

        // margin: 8px;
        display: flex;

        a {
          background-color: #20C58E;
          color: #ffffff;
          font-size: 1.2rem;
          font-weight: bold;
          padding: 8px 1.5rem;
          border-radius: 12px;
          margin: 8px 0 0 8px;
          width: 100%;

          &:hover {
            filter: brightness(95%);
            color: #20C58E;
          }
        }
      }
    }

  }

}

footer {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100dvw;
  height: 48px;
  background-color: var(--color-background-soft);
  display: flex;
  justify-content: center;
  align-items: center;
}

.main-content {
  width: calc(100dvw - 200px - 2rem);
  height: calc(100dvh - 64px - 48px);
  position: fixed;
  bottom: 48px;
  left: 180px;

  overflow: auto;
}

::-webkit-scrollbar {
  width: 10px;
  height: 10px;
}

::-webkit-scrollbar-thumb {
  background: var(--color-text);
  border-radius: 5px;
}

::-webkit-scrollbar-thumb:hover {
  background: var(--color-text);
}
</style>
