#include "proxylib.h"


int maxPlaintextSizeImpl(ZZn2 z)
{
    Big temp;
    z.get(temp);
	return bits(temp);
}