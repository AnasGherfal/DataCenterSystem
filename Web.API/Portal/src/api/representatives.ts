import type { RepresentativeModel } from "../Modules/Representatives/RepresentativesModel";
import { useHttpClient } from "../network/httpClient";

const httpClient = useHttpClient();

export const representativesApi = {
  get: async function () {
    const response = await httpClient.get(`/Representatives`);
    return response;
  },
  getById: async function (id: string) {
    const response = await httpClient.get(`/Representatives/${id}`);
    return response;
  },
  getFile: async function (id: string, fileId: string) {
    const response = await httpClient.get(
      `/Representatives/${id}/document/${fileId}`,
      {
        responseType: "arraybuffer",
      }
    );
    return response;
  },
  create: async function (representatives: FormData) {
    const response = await httpClient.post(
      "/Representatives",
      representatives,
      {
        headers: {
          "Content-Type": "multipart/form-data",
        },
      }
    );
    return response;
  },

};
