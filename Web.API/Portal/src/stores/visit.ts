import { visitApi } from "@/api/visits";
import { defineStore } from "pinia";
import { onMounted, ref } from "vue";

export const useVisitStore = defineStore("Visit", () => {
  const visitReasons = ref();
  onMounted(async () => {
    await getTypes();
  });
  async function getTypes() {  
    await visitApi
      .getTypes()
      .then(function (response) {
        visitReasons.value = response.data.content;

      })
      .catch(function (error) {
        console.log(error);
      });
    }


  return {
    visitReasons,
  };
});
