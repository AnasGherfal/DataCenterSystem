<script lang="ts" setup>
import Divider from "primevue/divider";
import { ref } from "vue";


const menu = ref();
const items = ref([
  { separator: true },
  { label: "حساب تعريفي", icon: "fa-solid fa-user", to: "/adminProfile" },
  { separator: true },
]);

const toggle = (event: any) => {
  menu.value.toggle(event);
};

const logout = () => {
  localStorage.removeItem("token");
  window.location.href = "/Login"; // Replace "/" with the actual home page URL
};
</script>

<template>
  <div
    class="topbar scalein animation-duration-1000 fixed z-5 flex align-items-center"
    style="
      -webkit-backdrop-filter: blur(5px);
      backdrop-filter: blur(5px);
      width: 100%;
      height: 40px;
      padding: 20px;
    "
  >
    <!-- <a href="/">
      <div>
        <img
          alt="logo"
          src="../assets/pics/LTT-Logo.png "
          height="37"
          style="margin-top: 27px; --fa-animation-iteration-count: 5;
          border-radius: 5px;"
          class="logo fa-bounce"
        />
      </div>
    </a>

    <div
      style="
        font-size: 20px;
        font-weight: 800;
        width: 257px;
        height: 40px;
        margin-right: 10px;
        margin-top: -20px;
      "
    >
      <a href="/" style="text-decoration: none; color: black">

        <img
          alt="logo"
          src="../assets/pics/LTT-Text.png "
          height="43"
          style="margin-top: 20px;"
        />
      </a>
    </div> -->

    <Divider layout="vertical" />
    <div
    class="inline-flex fadeinleft animation-duration-1000"
    style="height: 30px; margin-top: -18px; margin-right: 12px; margin-right: auto"
  >
    <!-- <Button @click="toggleThemeMode"   style="height: 40px; width: 40px; --fa-animation-iteration-count: 2;"  class="m-1 p-button-text" 
            icon="fa-solid fa-moon fa-fade" /> -->

    <RouterLink to="/SettingsView" style="text-decoration: none">
      <Button
        style="height: 40px; width: 40px; --fa-animation-iteration-count: 1"
        icon="fa-solid fa-gear fa-spin fa-spin-hover"
        class="m-1 p-button-text transition-colors transition-duration-500"
        v-tooltip="{ value: 'اعدادات النظام', fitContent: true }"
      />
    </RouterLink>

    <Button
      @click="toggle"
      style="height: 40px; width: 40px; --fa-animation-iteration-count: 2"
      icon="fa-solid fa-ellipsis-vertical"
      class="m-1 p-button-text fa-bounce-hover"
      aria-haspopup="true"
      aria-controls="overlay_menu"
      v-tooltip="{ value: 'قائمه', fitContent: true }"
    />
    <Menu ref="menu" :model="items" :popup="true">
      <template #start>
        <button
          @click=""
          class="w-full p-link flex align-items-center p-2 pl-3 text-color hover:surface-200 border-noround"
        >
          <span image="" class="mr-2" shape="circle" />
          <div class="flex flex-column align">
            <span class="font-bold">Admin</span>
          </div>
        </button>
      </template>
      <template #end>
        <!-- <ChangePasswordUser></ChangePasswordUser> -->

        <button
          @click="logout"
          class="w-full p-link flex align-items-center p-2 pl-4 text-color hover:surface-200 border-noround"
        >
          <i class="pi pi-sign-out" />
          <span class="ml-2">تسجيل الخروج</span>
        </button>
      </template>
    </Menu>
  </div>
    <!-- <IconGroup style="margin-right:calc(100% - 29rem);" /> -->
  </div>
</template>

<style scoped>
p {
  color: var(--primary-color);
}

@media screen and (max-width: 500px) {
  p {
    display: none;
  }
}

</style>
