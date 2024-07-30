.code
FilterASM proc
VPXOR XMM1, XMM1, XMM1						;czyœcimy xmm
VPXOR XMM0, XMM0, XMM0						;czyœcimy xmm
vpxor xmm2, xmm2, xmm2						;czyœcimy xmm
vpxor xmm3, xmm3, xmm3						;czyœcimy xmm
vpxor xmm4, xmm4, xmm4						;czyœcimy xmm
mov rax, 0015001500150015h					;do rax wk³adamy 21
movq mm0, rax								;do mm0 przenosimy rax
movq2dq xmm3, mm0							;do xmm3 przenosimy mm0
punpcklwd xmm3, xmm4						;rozpakowujemy wektor
MOVDQU XMM0, [RCX]							;pobieramy adres rcx do xmm0
movdqu xmm1, [rcx+12]						;pobieramy nastêpny pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+24]						;pobieramy 3 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+36]						;pobieramy 4 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+48]						;pobieramy 5 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+60]						;pobieramy 6 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+72]						;pobieramy 7 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+84]						;pobieramy 8 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+96]						;pobieramy 9 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+108]						;pobieramy 10 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+120]						;pobieramy 11 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+132]						;pobieramy 12 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+144]						;pobieramy 13 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+156]						;pobieramy 14 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+168]						;pobieramy 15 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+180]						;pobieramy 16 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+192]						;pobieramy 17 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+204]						;pobieramy 18 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+216]						;pobieramy 19 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+228]						;pobieramy 20 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+240]						;pobieramy 21 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+252]						;pobieramy 22 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+264]						;pobieramy 23 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+276]						;pobieramy 24 pixel
paddd xmm0, xmm1							;dodajemy pixele
movdqu xmm1, [rcx+288]						;pobieramy 25 pixel
paddd xmm0, xmm1							;dodajemy pixele
divps xmm0, xmm3							;dzielimy xmm0 przez 21
cvtps2dq xmm0, xmm0							;konwersja
movdqu [rcx], xmm0							;pobieramy do rcx xmm0
ret
FilterASM endp
end