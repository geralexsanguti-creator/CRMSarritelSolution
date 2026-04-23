import { clsx, type ClassValue } from "clsx";
import { twMerge } from "tailwind-merge";

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export const BACKEND_URL = "http://localhost:5066";

export function getUploadUrl(filename: string | null | undefined): string {
    if (!filename || filename === "default.png" || filename.trim() === "") return "";
    if (filename.startsWith("http")) return filename;
    return `${BACKEND_URL}/uploads/${filename}?t=${new Date().getTime()}`;
}
