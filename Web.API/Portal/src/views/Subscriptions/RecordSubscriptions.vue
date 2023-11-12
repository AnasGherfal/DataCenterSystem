<script setup lang="ts">
import { onMounted, reactive, ref } from "vue";
import { FilterMatchMode } from "primevue/api";
import { subscriptionApi } from "@/api/subscriptions";
import {formatDateTime} from "@/tools/formatTime"
  import {useStatusStore} from "@/stores/status"
const loading = ref();

const subId = ref();
const subscriptions = ref();
const statusStore = useStatusStore()

interface Filters {
  [key: string]: { value: string; matchMode: string };
}

const filters: Filters = reactive({
  global: { value: "", matchMode: FilterMatchMode.CONTAINS },
  status: { value: "", matchMode: FilterMatchMode.EQUALS },
});

async function getSubscriptions() {
  await subscriptionApi.get().then((response) => {
    // console.log(response);
    subscriptions.value = response.data.content;
  });
}
onMounted(async () => {
  await getSubscriptions();
});



</script>

<template>
    <RouterView></RouterView>
    <div v-if="$route.path === '/subscriptionsRecord'">   

  <Card>
    <template #title> سجل اشتركاتي </template>
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
          :value="subscriptions"
          dataKey="id"
          :paginator="true"
          :rows="10"
          v-model:filters="filters"
          :globalFilterFields="['name', 'status']"
          :rowsPerPageOptions="[5, 10, 25]"
          currentPageReportTemplate="  عرض {first} الى {last} من {totalRecords} عميل"
          paginatorTemplate="  "
        >
          <!-- <template #header>
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
          </template> -->

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
            field="serviceName"
            header="الإسم"
            style="min-width: 6rem"
            class="font-bold"
            frozen
          ></Column>
          
          <Column field="startDate" header="تاريخ البداية" style="min-width: 6rem">
            <template #body="{data}">
            {{ formatDateTime(data.startDate) }}</template>
          </Column>
          <Column field="endDate" header="تاريخ النهاية" style="min-width: 6rem">
            <template #body="{data}">
            {{ formatDateTime(data.endDate) }}
          </template>
          </Column>
          <Column
          field="totalPrice"
          header="اجمالي السعر"
          style="min-width: 6rem"
          >
        </Column>
        <Column field="status" header="الحالة" style="min-width: 6rem">
          <template #body="{data}">
            <Tag
            :value="statusStore.getSelectedStatusLabel(data.status)"
            :severity="statusStore.getSeverity(data.status)"
            
            ></Tag>
          
          </template>
        </Column>

        <Column style="min-width: 11rem">
            <template #body="slotProps">

              <RouterLink
                :key="slotProps.data.id"
                :to="'/subscriptionsRecord/SubscriptionsDetails/' + slotProps.data.id"
                style="text-decoration: none"
              >
                <Button
                  icon="fa-solid fa-circle-info"
                  severity="info"
                  text
                  rounded
                  v-tooltip="{ value: 'التفاصيل', fitContent: true }"
                />
              </RouterLink>
            </template>
          </Column>
        </DataTable>
      </div>
    </template>
  </Card>
  </div>
  </template>
