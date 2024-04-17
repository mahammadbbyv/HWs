import { useEffect, useState } from "react";
import { BlobServiceClient } from "@azure/storage-blob";

function Files({setIsLoading}) {
    const fetchFiles = async () => {
        setIsLoading(true);
        const blobItems = containerClient.listBlobsFlat(); 
        const urls = [];
        for await (const blob of blobItems) {
          const tempBlockBlobClient = containerClient.getBlockBlobClient(blob.name); 
          urls.push({ name: blob.name }); 
        }
        setImageUrls(urls);
    }
}