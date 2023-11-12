<script setup lang="ts">
import { ref } from "vue";
import Vue3autocounter from "vue3-autocounter";

const barData = ref([
  {
    name: " المجانية",
    color:'#1e7ecd',
    data: [44, 55, 21],
  },
  {
    name: "المدفوعة",
    color:'#2ba833',
    data: [13, 23, 21],
  },
]);

const pieData = ref([
  10,20
])


const donut = ref({
  chart:{
    type: 'donut',
    stacked:true,
    toolbar:{
      show:false,
    },
    stroke: {
    width: 2,
    colors: ["#fff"],
  },
  responsive: [
    {
      breakpoint: 1000,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
        },
      },
    },
  ],
  legend: {
    position: "bottom",
    offsetY: 5,
  },
    
  }
})

const bar = ref({
  chart: {
    type: "bar",
    stacked: true,
    toolbar: {
      show: false,
    },
    zoom: {
      enabled: true,
    },
    // dropShadow: {
    //   enabled: true,
    //   top: 0,
    //   left: 0,
    //   blur: 3,
    //   opacity: 0.5,
    // },
  },
  stroke: {
    width: 2,
    colors: ["#fff"],
  },
  responsive: [
    {
      breakpoint: 1000,
      options: {
        legend: {
          position: "bottom",
          offsetX: -10,
          offsetY: 0,
        },
      },
    },
  ],
  plotOptions: {
    bar: {
      horizontal: false,
      borderRadius: 5,
      dataLabels: {
        total: {
          enabled: true,
          style: {
            fontSize: "13px",
            fontWeight: 900,
          },
        },
      },
    },
  },
  xaxis: {
    type: "string",
    categories: [
      "Jan",
      "Feb",
      "March",
      "April",
      "May",
      "June",
      "July",
  
    ],
  },
  legend: {
    position: "bottom",
    offsetY: 5,
  },
  fill: {
    opacity: 1,
     colors: ['#0062a6', '#f4b205']
  },
});
</script>

<template>
  <div class="cards">
    <Card class="card card-two">
      <template #content>


        <div  style="display: flex; flex-wrap: wrap; " >
          <div
            class="background-icons"
            style="background-color: rgb(30, 126, 205)"
          >
            <i
              class="fa-solid fa-check"
              style="color: rgb(255, 255, 255); font-size: 2rem"
            ></i>
          </div>
          <div class="pp">
            <span>الزيارات</span>
            <Vue3autocounter
              ref="counter"
              :startAmount="0"
              :endAmount="10"
              :duration="3"
              prefix=""
              suffix="  "
              separator=","
              decimalSeparator=""
              :decimals="0"
              :autoinit="true"
            />
          </div>
        </div>
      </template>
    </Card>

    <Card class="card card-three">
      <template #content>
        <div style="display: flex; flex-wrap: wrap">
          <div
            class="background-icons"
            style="background-color: rgb(45, 168, 43)"
          >
            <i
              class="fa-solid fa-money-check"
              style="color: rgb(255, 255, 255); font-size: 2rem"
            ></i>
          </div>
          <div class="pp">
            <span> الاشتراكات</span>
            <Vue3autocounter
              ref="counter"
              :startAmount="0"
              :endAmount="10"
              :duration="3"
              prefix=""
              suffix="  "
              separator=","
              decimalSeparator=""
              :decimals="0"
              :autoinit="true"
            />
          </div>
        </div>

        <span class="text-green-500 font-medium"></span>
        <span class="text-500"></span>
      </template>
    </Card>

    <Card class="card card-four">
      <template #header>
         <!-- <div class="border-round-2xl">
            <div class="flex justify-content-between mb-1">
              <div>
                <span class="block text-green-600 font-medium font-semibold"
                  >kkk
                </span>
              </div>
  
            </div>
          </div> -->

          <!-- <div class="text-white-900 font-medium text-xl">
            <Vue3autocounter
              ref="counter"
              :startAmount="0"
              :endAmount="10"
              :duration="3"
              prefix=""
              suffix=" اشتراك "
              separator=","
              decimalSeparator=""
              :decimals="0"
              :autoinit="true"
            />
          </div>  -->

        <span class="text-green-500 font-medium"></span>
        <span class="text-500"></span>
      </template>
    </Card>

    <Card class="card">
      <template #header>
         <!-- <div class="border-round-2xl">
            <div class="flex justify-content-between mb-1">
              <div>
                <span class="block text-green-600 font-medium font-semibold"
                  >kkk
                </span>
              </div>
  
            </div>
          </div> -->

          <!-- <div class="text-white-900 font-medium text-xl">
            <Vue3autocounter
              ref="counter"
              :startAmount="0"
              :endAmount="10"
              :duration="3"
              prefix=""
              suffix=" اشتراك "
              separator=","
              decimalSeparator=""
              :decimals="0"
              :autoinit="true"
            />
          </div>  -->

        <span class="text-green-500 font-medium"></span>
        <span class="text-500"></span>
      </template>
    </Card>
    

    <Card class="card card-one">
      <template #content>
        <div class="chart-container" style="display: flex; flex-wrap: wrap">
          <div class="pp">
            <span>الزيارات المتبقية </span>
          </div>
        </div>
        <VueApexCharts
          width="90%"
          height="300"
          class="chart"
          type="bar"
          :options="bar"
          :series="barData"
        ></VueApexCharts>
      </template>
    </Card>
    <Card class="card ">
      <template #content>
        <div class="pie-container" style="display: flex; flex-wrap: wrap">
          <div class="pp">
            <span>الزيارات المتبقية </span>
          </div>
        </div>
        <VueApexCharts
          width="100%"
          height="300"
          class="chart"
          type="donut"
          :options="donut"
          :series="pieData"
        ></VueApexCharts>
      </template>
    </Card>
  </div>
</template>

<style scoped>
.pp {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 5px;
  font-size: 1rem;
  font-weight: bold;
  margin-right: 20px;
  color:white
}

::v-deep .p-card .p-card-body {
  padding: 0rem !important;
}
.background-icons {
  border-radius: 0.25rem;
  display: flex;
  align-items: center;
  justify-content: center;
  width: 50px;
  margin-right: 20px;
}
.cards {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 1rem; /* Add space between cards */
}

.card {
  border-radius: 10px;
  font-size: var(--fs-400);
}

.card:first-child {
  background-color: #1e7ecd;
  grid-column: span 1;
  grid-row: 1;
}

.card:nth-child(2) {
  background-color: #2ba833;
  opacity: 80%;
  grid-column:  2;
  grid-row: 1;
}

.card:nth-child(3) {
  grid-column:  3;
  grid-row:  1;
}
.card:nth-child(4) {
  grid-column:  4;
  grid-row:  1;
}

.card:nth-child(5) {
  grid-row: span 4;
  grid-column: span 2;
  background-color: rgb(255, 255, 255);
}

.card:last-child{
  grid-column:  3 ;
  grid-row:  span 2;
}
</style>
