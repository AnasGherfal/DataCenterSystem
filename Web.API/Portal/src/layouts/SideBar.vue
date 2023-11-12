<script lang="ts" setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

const items = ref([
  {
    label: "لوحة المعلومات",
    items: [
      {
        label: "الصفحة الرئيسية",
        icon: "fa-solid fa-house",
        to: "/",
      },
    ],
  },

  {
    label: "السجلات",
    items: [
      {
        label: " مخوليني",
        icon: "fa-solid fa-users",
        command: () => {
          router.push("/representativesRecord");
        },

      },

      {
        label: " زياراتي",
        icon: "fa-solid fa-book",
        to: "/visitsRecord",
      },

      {
        label: " اشتركاتي",
        icon: "fa-solid fa-calculator",
        to: "/subscriptionsRecord",
      },
      // {
      //   label: "  فواتيري",
      //   icon: "fa-solid fa-receipt",
      //   to: "/invoices",
      // },
    ],
  },
]);

// hasPermission(store)
const menu = ref();

const toggle = (event: any) => {
  menu.value.toggle(event);
};
</script>

<template>
  <Menu
    ref="menu"
    :model="items"
    :popup="false"
    class="sidebar overflow-auto fixed mt-4 fadeinright animation-duration-500"
    style="border-radius: 15px; width: 22%; height: calc(110vh - 7.6rem)"
  >
    <template #start>
      <div  style="margin-top: 1.5rem; margin-bottom: 0.5rem; display: grid; justify-content: center; justify-items: center;">
        <img
          alt="logo"
          src="../assets/pics/LTT-Logo.png "
          height="45"
          style="
            margin-right: 0.5rem;
            margin-bottom: 1rem;

            --fa-animation-iteration-count: 5;
            border-radius: 5px;
          "
          class="logo fa-bounce"
        />
        <!-- <Avatar image="../assets/pics/LTT-Logo.png" class="mr-2" shape="circle" /> -->
        <a href="/" style="text-decoration: none; color: black">
          <img
            alt="logo"
            src="../assets/pics/LTT-Text.png "
            height="40"
            style="margin-right: 10px"
          />
        </a>
        <!-- <span class="font-bold">LTT </span> -->
      </div>
    </template>
  </Menu>

  <div>
    <Button
      style="display: none"
      class="SideBarbutton fixed"
      type="button"
      icon="fa-solid fa-bars"
      text
      aria-label="Filter"
      @click="toggle"
      aria-haspopup="true"
      aria-controls="overlay_menu"
      v-tooltip="{ value: 'الشريط الجانبي', fitContent: true }"
    />

    <Menu ref="menu" id="overlay_menu" :model="items" :popup="true"> </Menu>
  </div>
</template>

<style lang="scss">
.p-menuitem:hover {
  background-color: none !important;
}

.p-menuitem-content {
  margin: 10px;
}
.p-menuitem-text {
  color: var(--primary-color);

  margin-right: 5px;
  font-family: "Tajawal";
}

.p-submenu-header {
  font-family: "tajawal";
  font-weight: 800 !important;
}
.p-menuitem {
  .router-link-active {
    border-radius: 20px;
    border-color: #175deb46;
    border-style: solid;
    border-width: 3px;
  }
}
.p-menuitem-link:hover {
  border-radius: 20px;
}
.router-link-active {
  .p-menuitem-icon {
    color: var(--primary-color) !important;
  }
  .p-menuitem-text {
    font-weight: 600;
  }
}

.p-menu
  .p-menuitem:not(.p-highlight):not(.p-disabled)
  > .p-menuitem-content:hover {
  background-color: rgba(84, 118, 199, 0.386);
  border-radius: 20px;
}

.p-menu
  .p-menuitem:not(.p-highlight):not(.p-disabled).p-focus
  > .p-menuitem-content {
  background-color: rgba(84, 118, 199, 0.386);
  border-radius: 20px;
}

@media screen and (max-width: 600px) {
  .sidebar {
    display: none;
  }
  .sidebar {
    display: none;
  }
  .SideBarbutton {
    display: inline !important;
    top: 60px;
  }
}

// class="p-menuitem-link router-link-active router-link-active-exact"
</style>
