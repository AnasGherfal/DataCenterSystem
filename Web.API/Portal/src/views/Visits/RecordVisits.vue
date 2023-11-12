<script setup lang="ts">
import { onMounted, reactive, ref } from "vue";
import { FilterMatchMode } from "primevue/api";
import { visitApi } from "@/api/visits";
import AddButton from "@/components/AddButton.vue"
import { useVisitStore } from "@/stores/visit";
import { formatTotalMin } from "@/tools/formatTime";
import { formatDateTime } from "@/tools/formatTime";

const loading = ref();

const name = ref();
const store = useVisitStore();
const visits = ref();

interface Filters {
  [key: string]: { value: string; matchMode: string };
}

const filters: Filters = reactive({
  global: { value: "", matchMode: FilterMatchMode.CONTAINS },
  status: { value: "", matchMode: FilterMatchMode.EQUALS },
});

async function getVisits() {
  await visitApi
  .get()
  .then((response)=>{
    // console.log(response);
    visits.value= response.data.content;
  })
}
onMounted(async() => {
 await getVisits();

})
</script>

<template>
      <RouterView></RouterView>

<div v-if="$route.path === '/visitsRecord'">
  <Card>
    <template #title> سجل زياراتي 
      <AddButton name-button="إضافة مخول" rout-name="/visitsRecord/requestVisit" />

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
          :value="visits"
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
            field="visitType"
            header="سبب الزيارة"
            style="min-width: 6rem"
            class="font-bold"
            frozen
          >
          <template #body="{ data }">
              <Tag severity="info"
                :value="
                  store.visitReasons && store.visitReasons[data.visitType - 1]
                    ? store.visitReasons[data.visitType - 1].name
                    : ''
                "
              >
              </Tag>
            </template></Column>
          <Column field="visitStatus" header="الحالة" style="min-width: 6rem">
          </Column>
          <Column field="expectedStartTime" header="وقت البداية" style="min-width: 6rem">
            <template #body="{data}">
            {{ formatDateTime(data.expectedStartTime) }}</template>
          </Column>
          <Column field="expectedEndTime" header="وقت نهاية" style="min-width: 6rem">
            <template #body="{data}">
            {{ formatDateTime(data.expectedEndTime) }}</template>
          </Column>

          <Column field="price" header="السعر" style="min-width: 6rem">
          </Column>
          <Column field="totalMinutes" header="مدة الزيارة" style="min-width: 6rem">
            <template #body="{ data }">
              {{
                data?.totalMinutes ? formatTotalMin(data.totalMinutes) : "- "
              }}
            </template>
          </Column>
        </DataTable>
      </div>
    </template>
  </Card>
  </div>
</template>
