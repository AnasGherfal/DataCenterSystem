<script setup lang="ts">
import { onMounted, reactive, ref } from "vue";
import { FilterMatchMode } from "primevue/api";
import {representativesApi} from "@/api/representatives"
import AddButton from "@/components/AddButton.vue"
import { useStatusStore } from "@/stores/status";
const loading = ref();

const name = ref();
const representatives = ref()
const store = useStatusStore();
// const representatives:RepresentativeModel = [
//   {
//     firstName: "",
//   lastName: "",
//   identityNo: "",
//   identityType: 0,
//   email: "",
//   phoneNo: "",
//   status: 0,
//   createdOn: "",
//   },
// ];

interface Filters {
  [key: string]: { value: string; matchMode: string };
}

const filters: Filters = reactive({
  global: { value: "", matchMode: FilterMatchMode.CONTAINS },
  status: { value: "", matchMode: FilterMatchMode.EQUALS },
});




async function getRepresentatives() {
  await representativesApi
  .get()
  .then((response)=>{
    // console.log(response);
    representatives.value= response.data.content;
  })
}
onMounted(async() => {
 await getRepresentatives();

})
</script>

<template>
      <RouterView></RouterView>

      <div v-if="$route.path === '/representativesRecord'">

  <Card>
    <template #title> سجل مخولين 

      <AddButton name-button="إضافة مخول" rout-name="/representativesRecord/CreateRepresentative" />
    </template>
    <template #content>
      <div>
        <div
          v-if="loading"
          class="border-round border-1 surface-border p-4 surface-card"
        >
          <div class="grid p-fluid">
            <div v-for="n in 9" class="field col-12 md:col-6 lg:col-4">
              <span class="p-float-label">
                <Skeleton width="100%" height="2rem"></Skeleton>
              </span>
            </div>
          </div>

          <Skeleton width="100%" height="100px"></Skeleton>
          <div class="flex justify-content-between mt-3">
            <Skeleton width="100%" height="2rem"></Skeleton>
          </div>
        </div>

        <DataTable
          v-else
          ref="dt"
          :value="representatives"
          dataKey="id"
          :paginator="true"
          :rows="10"
          v-model:filters="filters"
          :globalFilterFields="['name', 'status']"
          :rowsPerPageOptions="[5, 10, 25]"
          currentPageReportTemplate="  عرض {first} الى {last} من {totalRecords} عميل"
          paginatorTemplate="  "
        >
          <template #header>
            <div class="grid p-fluid mt-1">
              <div class="field col-12 md:col-6 lg:col-4">
                <div
                  class="table-header flex flex-column md:flex-row justiify-content-between"
                >
                  <span class="p-input-icon-left p-float-label">
                    <i class="fa-solid fa-magnifying-glass" />
                    <InputText
                      ref="searchInput"
                      v-model="name"
                      placeholder="البحث"
                    />
                    <label for="search" style="font-weight: lighter">
                      البحث
                    </label>
                  </span>
                </div>
              </div>
            </div>
          </template>

          <template #empty>
            <div
              class="no-data-message"
              style="
                height: 100px;
                display: flex;
                flex-direction: column;
                align-items: center;
                justify-content: center;
                padding: 20px;
                background-color: #f9f9f9;
                border-radius: 5px;
                box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
              "
            >
              <p style="font-size: 18px; font-weight: bold; color: #888">
                لا يوجد بيانات
              </p>
            </div>
          </template>

          <Column
            field="firstName"
            header=" الإسم"
            style="min-width: 6rem"
            class="font-bold"
            frozen
          ></Column>
          <Column
            field="lastName"
            header="اللقب"
            style="min-width: 6rem"
            class="font-bold"
            frozen
          ></Column>
          <Column field="status" header="الحالة" style="min-width: 6rem">
            <template #body="{data}">
              <Tag
              :value="store.getSelectedStatusLabel(data.status)"
              :severity="store.getSeverity(data.severity)">

              </Tag>
            
            </template>
          </Column>

          <Column field="email" header="البريد" style="min-width: 6rem">
          </Column>
          <Column field="phoneNo" header="رقم الهاتف" style="min-width: 6rem">
          </Column>

          <Column style="min-width: 11rem" header="  الاجراءات ">
            <template #body="slotProps">
              <!-- <span v-if="slotProps.data.status !== 2">
                  <Delete
                    :name="slotProps.data.name"
                    :id="slotProps.data.id"
                    @submit="() => deleteRepresetative(slotProps.data.id)"
                    type="Customers"
                  >
                  </Delete>
                </span> -->
              <RouterLink
                :to="
                  'representativesRecord/DetailsRepresentatives/' +
                  slotProps.data.id
                "
              >
                <Button
                  v-tooltip="{ value: 'البيانات الشخصية', fitContent: true }"
                  icon="fa-solid fa-user"
                  severity="info"
                  text
                  rounded
                  aria-label="Cancel"
                />
              </RouterLink>
              <!-- <LockButton
                typeLock="Representatives"
                :id="slotProps.data.id"
                :name="slotProps.data.name"
                :status="slotProps.data.status"
              /> -->
            </template>
          </Column>
        </DataTable>
      </div>
    </template>
  </Card>
  </div>
</template>
