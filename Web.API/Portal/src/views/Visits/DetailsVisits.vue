<script setup lang="ts">
import { ref } from 'vue';


const visits = ref(
  {
    id: "",
    expectedStartTime: "",
    expectedEndTime: "",
    totalMinutes: "",
    price: 0,
    notes: "",
    visitType:0,
    visitStatus:"",
    createdOn:""

  },
);

</script>