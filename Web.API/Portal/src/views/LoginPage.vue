<script setup lang="ts">
import { computed, reactive, ref } from "vue";
import Password from "primevue/password";
import { authApi } from "../api/auth";
import { useRouter } from "vue-router";
import { email, helpers, required } from "@vuelidate/validators";
import useVuelidate from "@vuelidate/core";
import { useToast } from "primevue/usetoast";
import backgroundImage from "../assets/pics/loginImge.jpg";

const credentials = reactive({
  email: "",
  password: "",
});
const toast = useToast();
const router = useRouter();
const loading = ref(false);

const submitForm = async () => {
  const result = await v$.value.$validate();
  if (result) {
    loading.value = true;
    authApi
      .login(credentials)
      .then((response) => {
        // Store the token if the login is successful
        const token = response.data.content.accessToken;
        localStorage.setItem("token", token);

        router.push({ name: "home" });
      })
      .catch((error) => {
        toast.add({
          severity: "error",
          summary: "رسالة خطأ",
          detail: `${error}`,
          life: 3000,
        });
      })
      .finally(() => {
        loading.value = false;
      });
  }
};

const rules = computed(() => {
  return {
    email: {
      required: helpers.withMessage("البريد الأكتروني مطلوب", required),
      email: helpers.withMessage(" ليس عنوان بريد إلكتروني صالح", email),
    },
    password: { required: helpers.withMessage("كلمة المرور مطلوبة", required) },
  };
});
const v$ = useVuelidate(rules, credentials);
</script>

<template>
  <div id="backg">
    <div class="background-container">
      <div class="right-section">
        <div class="above-input-text" style="display: grid;justify-items: center;color:white ">
            <h2>تسجيل الدخول</h2>
            <h5>سجل دخولك الى نظام إدارة مركز البيانات</h5>
          </div>
        <div class="grid p-fluid">
          <div class="field col-12 md:col-12 lg:col-12">
            <span class="p-input-icon-left">
              <InputText
                id="email"
                name="email"
                placeholder="البريد الالكتروني"
                type="email"
                v-model="credentials.email"
              />
              <div style="height: 2px">
                <span
                  v-for="error in v$.email.$errors"
                  :key="error.$uid"
                  style="color: red; font-weight: bold; font-size: small"
                >
                  {{ error.$message }}</span
                >
              </div>
            </span>
          </div>
          <div class="field col-12 md:col-12 lg:col-12">
            <span class="p-input-icon-left">
              <Password
                v-model="credentials.password"
                placeholder="كلمة المرور"
                :feedback="false"
              />
              <div style="height: 2px">
                <span
                  v-for="error in v$.password.$errors"
                  :key="error.$uid"
                  style="color: red; font-weight: bold; font-size: small"
                >
                  {{ error.$message }}</span
                >
              </div>
            </span>
          </div>
        </div>
        <Button
        
          @click="submitForm"
          style="width:40%; background-color: white; color: navy;"
          label="تسجيل دخول"
          type="submit"
          :loading="loading"
        />
      </div>

      <div class="vertical-line"></div>

      <div class="left-section">
        <div class="image-container" style="opacity: 0.7">
          <!-- Adjust opacity as needed -->
          <img src="../assets/pics/login-logo.png" alt="Login Logo" />
        </div>
        <div class="text-content">
          <h1 class="header-text">مرحباً بك..</h1>
          <span
            >في نطام إدارة مركز البيانات </span
          >
          <span
            >لشركة ليبيا للأتصالات والتقنية </span
          >
        </div>
      </div>
    </div>
    <Toast position="bottom-left" />
  </div>
</template>

<style scoped>
#backg {
  background: url(../assets/pics/loginImge.jpg) no-repeat center center fixed;
  background-size: cover;
  

  height: 100vh;
  width: 100vw;
  display: flex;
  justify-content: center;
  align-items: center;
  filter: brightness(80%);
}

.background-container {
  display: flex;
  align-items: center;

  width: 100%;
  height: 100%;
}

.left-section {
  background: rgba(0, 0, 0, 0.3); /* Adjust opacity (0.5) as needed */

  display: flex; /* Display the content in a flex container */
  align-items: flex-end;
  color: white;
  flex: 1;
  padding: 0;
  text-align: center;
  height: 100%;
  position: relative;
}

.image-container img {
  width: 70%;
  height: 100%;
  opacity:50% ;
  display: flex;
  margin-right: 10%;
}

.text-content {
  position: absolute; /* Add absolute positioning */
  top: 50%; /* Center vertically */
  left: 50%; /* Center horizontally */
  transform: translate(-50%, -50%); /* Center text */
  padding: 20px;
  text-align: center;
  width: 40vw;
  color:white
}

.vertical-line {
  width: 5px;
  background-color: rgb(255, 255, 255); /* Adjust opacity (0.5) as needed */
  height: 550px;
  border-radius: 10px;
}

.right-section {
  background: rgba(0, 0, 0, 0.6); /* Adjust opacity (0.5) as needed */
  height: 100%;
  display: flex;
  align-items: center;
  align-content: center;
  justify-content: center;
  flex-wrap: wrap;

  flex: 1;
  padding: 20px; /* Adjust padding as needed */
}

h1 {
  color: #ffffff;
  font-size: 70px;
  margin: 0;
}

span {
  color: rgb(255, 255, 255);
  font-size: 25px;
  display: block;
}

.p-input-icon-right > i:last-of-type {
  right: 10.8rem;
  color: black;
}
</style>
