import { HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { environment } from "environments/environment";

export function apiBaseUrlInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
  const baseUrl = environment.apiUrl;

  const newReq = req.clone({
    url: baseUrl + req.url,
  });

  return next(newReq);
}
