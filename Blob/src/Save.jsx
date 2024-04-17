import { BlobServiceClient } from "@azure/storage-blob";
import { useState } from "react";

function Save({setIsLoading}) {
    const [file, setFile] = useState(null);
    const blobServiceClient = new BlobServiceClient();
    const containerClient = blobServiceClient.getContainerClient();
    async function uploadFile(){
        setIsLoading(true);
        const blobName = `${new Date().getTime()}-${file.name}`; 
        const blobClient = containerClient.getBlockBlobClient(blobName); 
        console.log(blobClient)
        console.log(file)
        await blobClient.uploadData(file, { blobHTTPHeaders: { blobContentType: file.type } });
        setIsLoading(false);
    }
    return (
        <>
            <input type="file" onChange={(e) => setFile(e.target.files[0])} />
            <button onClick={() => uploadFile()}>Upload</button>
        </>
    )
}

export default Save