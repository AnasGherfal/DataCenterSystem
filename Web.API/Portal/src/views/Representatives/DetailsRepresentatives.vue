<script setup lang="ts">
import { computed, onBeforeUnmount, onMounted, ref } from "vue";
import { representativesApi } from "../../api/representatives";
import { useRoute } from "vue-router";
import { useToast } from "primevue/usetoast";
import BackButton from "../../components/BackButton.vue";
import { useStatusStore } from "@/stores/status";
const route = useRoute();
const toast = useToast();

const loading = ref();
const disabled = ref(true);
const screenWidth = ref(window.innerWidth);
const store = useStatusStore()
const representatives = ref({
  id: "",

  firstName: "",
  lastName: "",
  email: "",
  identityNo: "",
  identityType: 0,
  phoneNo: "",
  files: [
    { fileType: 0, id: "" },
    { fileType: 0, id: "" },
  ],
});

const userId = computed(() => {
  if (route && route.params && route.params.id) {
    return String(route.params.id); // Convert the ID to a number
  } else {
    return ""; // or return a default value if id is not available, such as -1
  }
});
const updateScreenWidth = () => {
  screenWidth.value = window.innerWidth;
};

onMounted(() => {
  window.addEventListener("resize", updateScreenWidth);
});

onBeforeUnmount(() => {
  window.removeEventListener("resize", updateScreenWidth);
});

async function getRepresentatives() {
  await representativesApi.getById(userId.value).then((response) => {
    console.log(response);
    representatives.value = response.data.content;
  });
}

onMounted(async () => {
  await getRepresentatives();
});

const downloadFile = async (id: any, fileId: string) => {
  try {
    const response = await representativesApi.getFile(id, fileId);

    if (response) {
      const blob = new Blob([response.data], {
        type: "application/octet-stream",
      });
      const url = URL.createObjectURL(blob);

      const link = document.createElement("a");
      link.href = url;
      link.download = "downloaded_file.png"; // Set the desired downloaded filename here
      document.body.appendChild(link);
      link.click();
      document.body.removeChild(link);

      URL.revokeObjectURL(url);
    } else {
      console.error("Invalid file response");
    }
  } catch (error) {
    console.error("Error downloading file:", error);
    toast.add({
      severity: "error",
      summary: "خطأ",
      detail: "حدث خطأ أثناء تحميل الملف",
      life: 3000,
    });
  }
};
</script>

<template>
  <div>
    <Card>
      <template #title>
        بيانات المخول

        <BackButton style="float: left" />

        <span
          style="width: 30px; height: 30px; margin-right: 10px; margin-top: 0px"
        >
          <!-- <DeleteCustomer
              :name="props.customer.name"
              :id="props.customer.id"
            ></DeleteCustomer> -->
        </span>
        <Divider />
      </template>
      <template #content>
        <div v-if="loading">
          <div class="grid p-fluid">
            <div class="field col-12 md:col-6 lg:col-4">
              <span class="p-float-label">
                <Skeleton
                  :loading="loading"
                  width="100%"
                  height="2rem"
                ></Skeleton>
              </span>
            </div>
            <div class="field col-12 md:col-6 lg:col-4">
              <span class="p-float-label">
                <Skeleton
                  :loading="loading"
                  width="100%"
                  height="2rem"
                ></Skeleton>
              </span>
            </div>
            <div class="field col-12 md:col-6 lg:col-4">
              <span class="p-float-label">
                <Skeleton
                  :loading="loading"
                  width="100%"
                  height="2rem"
                ></Skeleton>
              </span>
            </div>
            <div class="field col-12 md:col-6 lg:col-4">
              <span class="p-float-label">
                <Skeleton
                  :loading="loading"
                  width="100%"
                  height="2rem"
                ></Skeleton>
              </span>
            </div>
            <div class="field col-12 md:col-6 lg:col-4">
              <span class="p-float-label">
                <Skeleton
                  :loading="loading"
                  width="100%"
                  height="2rem"
                ></Skeleton>
              </span>
            </div>
            <div class="field col-12 md:col-6 lg:col-4">
              <Skeleton
                :loading="loading"
                width="100%"
                height="2rem"
              ></Skeleton>
            </div>
          </div>
        </div>
        <div v-else>
          <div>
            <div class="grid p-fluid">
              <div class="field col-12 md:col-6 lg:col-4">
                <span class="p-float-label">
                  <InputText
                    id="firstName"
                    type="text"
                    v-model="representatives.firstName"
                    :disabled="disabled"
                  />

                  <label for="name">اللقب </label>
                </span>
              </div>

              <div class="field col-12 md:col-6 lg:col-4">
                <span class="p-float-label">
                  <InputText
                    id="lastName"
                    type="text"
                    v-model="representatives.lastName"
                    :disabled="disabled"
                  />

                  <label for="name">البريد </label>
                </span>
              </div>
              <div class="field col-12 md:col-6 lg:col-4">
                <span class="p-float-label">
                  <InputText
                    id="email"
                    type="text"
                    v-model="representatives.email"
                    :disabled="disabled"
                  />
                  <label for="email">البريد الإلكتروني</label>
                </span>
              </div>

              <div class="field col-12 md:col-6 lg:col-4">
                <span class="p-float-label">
                  <Dropdown
                    id="identityType"
                    type="text"
                    :options="store.identityTypeOptions"

                    v-model="representatives.identityType"
                    optionValue="value"
            optionLabel="text"

                    :disabled="disabled"
                  />
                  <label for="address">نوع الهوية</label>
                </span>
              </div>
              <div class="field col-12 md:col-6 lg:col-4">
                <span class="p-float-label">
                  <InputText
                    id="identityNo"
                    type="text"
                    v-model="representatives.identityNo"
                    :disabled="disabled"
                  />
                  <label for="address">رقم الهوية</label>
                </span>
              </div>
              <div class="field col-12 md:col-6 lg:col-4">
                <span class="p-float-label">
                  <InputMask
                    id="phoneNum1"
                    v-model="representatives.phoneNo"
                    mask="+218999999999"
                    style="direction: ltr; text-align: end"
                    :disabled="disabled"
                  />
                  <label for="phoneNum1">رقم هاتف </label>
                </span>
              </div>
            </div>
            <div for="files" style="margin-bottom: 2rem">الملفات</div>
            <div class="grid p-fluid">
              <div class="field col-4 md:col-4 lg:col-4">
                <span class="p-float-label">
                  <InputText
                    class="p-inputtext p-component"
                    v-model="representatives.files[0].id"
                    :disabled="true"
                  />

                  <label for="secondaryPhone">تعريف شخصي</label>
                </span>
              </div>

              <div
                class="file-actions field col-4 md:col-4 lg:col-4"
                v-if="screenWidth >= 768"
              >
                <Button
                  @click="
                    downloadFile(
                      representatives.id,
                      representatives.files[0].id
                    )
                  "
                  icon="fa-solid fa-download"
                  class="p-button-text p-button-info"
                >
                  تحميل
                </Button>
              </div>

              <div
                class="file-actions inline-block field col-4 md:col-4 lg:col-4"
                v-else
              >
                <Button
                  @click="
                    downloadFile(
                      representatives.id,
                      representatives.files[0].id
                    )
                  "
                  icon="fa-solid fa-download"
                  class="p-button-icon-only p-button-info"
                  text
                  v-tooltip.top="{
                    value: 'تحميل',
                    fitContent: true,
                  }"
                />
              </div>
            </div>
            <div class="grid p-fluid">
              <div class="field col-4 md:col-4 lg:col-4">
                <span class="p-float-label">
                  <InputText
                    class="p-inputtext p-component"
                    v-model="representatives.files[1].id"
                    :disabled="true"
                  />

                  <label for="fileName">تخويل من الشركة</label>
                </span>
              </div>

              <div
                class="file-actions field col-4 md:col-4 lg:col-4"
                v-if="screenWidth >= 768"
              >
                <Button
                  @click="
                    downloadFile(
                      representatives.id,
                      representatives.files[1].id
                    )
                  "
                  icon="fa-solid fa-download"
                  class="p-button-text p-button-info"
                >
                  تحميل
                </Button>
              </div>
              <div
                class="file-actions inline-block field col-4 md:col-4 lg:col-4"
                v-else
              >
                <!-- <Button
                      @click="downloadFile(customer.id, customer.files[1].id)"
                      icon="fa-solid fa-download"
                      class="p-button-icon-only p-button-info"
                      text
                      v-tooltip.top="{
                        value: 'تحميل',
                        fitContent: true,
                      }"
                    />
  
                    <Button
                      v-if="!actEdit"
                      icon="fa-solid fa-upload"
                      class="p-button-icon-only p-button-info"
                      text
                      @click="triggerFileInput(1)"
                      v-tooltip.top="{
                        value: 'رفع ملف',
                        fitContent: true,
                      }"
                    >
                    </Button> -->
              </div>
            </div>
          </div>
          <!-- <div v-if="!actEdit">
              <Button
                @click="onFormSubmit"
                icon="fa-solid fa-check"
                label="تعديل"
              />
              <Button
                @click="actEdit = !actEdit"
                icon="fa-solid fa-ban"
                label="إلغاء التعديل"
                class="p-button-danger"
                style="margin-right: 0.5em"
              />
            </div> -->
          <Toast position="bottom-left" />
        </div>
      </template>
    </Card>
  </div>
</template>
